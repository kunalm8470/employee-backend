using Employees.Api.Models.v1.Employee.Requests;
using Employees.Api.Models.v1.Employee.Responses;
using Employees.Domain.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;
using Xunit;

namespace Employees.IntegrationTests.Controllers.v1
{
    public class EmployeeControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        private readonly JsonSerializerOptions _jsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public EmployeeControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Theory]
        [InlineData(1, 2)]
        [InlineData(2, 2)]
        public async Task ListEndpoint_WhenPassedValidPageAndLimit_ReturnsSuccessAndCorrectData(int page, int limit)
        {
            // Arrange
            Dictionary<string, string> listQueryParams = new()
            {
                ["page"] = page.ToString(),
                ["limit"] = limit.ToString()
            };

            string listUri = QueryHelpers.AddQueryString("/api/v1/Employees", listQueryParams);

            // Act
            HttpResponseMessage response = await _client.GetAsync(listUri);

            // Assert
            response.EnsureSuccessStatusCode();

            string stringResponse = await response.Content.ReadAsStringAsync();
            PaginatedList<EmployeeDto> employees = JsonSerializer.Deserialize<PaginatedList<EmployeeDto>>(stringResponse);

            Assert.Equal(limit, employees.PageSize);
            Assert.Equal(page, employees.PageIndex);
        }

        [Theory]
        [InlineData(int.MinValue, int.MinValue)]
        [InlineData(-1, -1)]
        [InlineData(-1, 1)]
        [InlineData(1, -1)]
        [InlineData(0, 0)]
        public async Task ListEndpoint_WhenPassedInvalidPageAndLimit_ReturnsBadRequest(int page, int limit)
        {
            // Arrange
            Dictionary<string, string> listQueryParams = new()
            {
                ["page"] = page.ToString(),
                ["limit"] = limit.ToString()
            };

            string listUri = QueryHelpers.AddQueryString("/api/v1/Employees", listQueryParams);

            // Act
            HttpResponseMessage response = await _client.GetAsync(listUri);

            // Assert
            string stringResponse = await response.Content.ReadAsStringAsync();
            ProblemDetails errorResponse = JsonSerializer.Deserialize<ProblemDetails>(stringResponse);
            JsonNode errors = JsonNode.Parse(errorResponse.Extensions["errors"].ToString());

            Assert.NotNull(errorResponse.Status);
            Assert.NotNull(errorResponse.Title);
            Assert.NotNull(errors);
            Assert.Equal(StatusCodes.Status400BadRequest, errorResponse.Status);
            Assert.Equal("One or more validation errors occurred.", errorResponse.Title);
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        public async Task GetByIdEndpoint_WhenPassedInvalidEmployeeId_ReturnsNotFound(int id)
        {
            // Arrange
            string getByIdUri = $"/api/v1/Employees/{id}";

            // Act
            HttpResponseMessage response = await _client.GetAsync(getByIdUri);

            // Assert
            string stringResponse = await response.Content.ReadAsStringAsync();
            ProblemDetails errorResponse = JsonSerializer.Deserialize<ProblemDetails>(stringResponse);

            Assert.NotNull(errorResponse.Status);
            Assert.NotNull(errorResponse.Title);
            Assert.Equal(StatusCodes.Status404NotFound, errorResponse.Status);
            Assert.Equal("Not Found", errorResponse.Title);
        }

        [Theory]
        [InlineData(1)]
        public async Task GetByIdEndpoint_WhenPassedValidEmployeeId_ReturnsSuccessAndCorrectData(int id)
        {
            // Arrange
            string getByIdUri = $"/api/v1/Employees/{id}";

            // Act
            HttpResponseMessage response = await _client.GetAsync(getByIdUri);

            // Assert
            string stringResponse = await response.Content.ReadAsStringAsync();
            EmployeeDto employee = JsonSerializer.Deserialize<EmployeeDto>(stringResponse);

            response.EnsureSuccessStatusCode();
            Assert.NotNull(employee);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidCreateEmployeeObjects))]
        public async Task CreateEndpoint_WhenPassedInvalidEmployeeObject_ReturnsBadRequest(CreateEmployee employee)
        {
            // Arrange
            string createUri = "/api/v1/Employees";
            StringContent jsonContent = new(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync(createUri, jsonContent);

            // Assert
            string stringResponse = await response.Content.ReadAsStringAsync();
            ProblemDetails errorResponse = JsonSerializer.Deserialize<ProblemDetails>(stringResponse);
            JsonNode errors = JsonNode.Parse(errorResponse.Extensions["errors"].ToString());

            Assert.NotNull(errorResponse.Status);
            Assert.NotNull(errorResponse.Title);
            Assert.NotNull(errors);
            Assert.Equal(StatusCodes.Status400BadRequest, errorResponse.Status);
            Assert.Equal("One or more validation errors occurred.", errorResponse.Title);
        }

        [Fact]
        public async Task CreateEndpoint_WhenPassedValidEmployeeObject_ReturnsCreatedWithLocationHeaderSet()
        {
            // Arrange
            string createUri = "/api/v1/Employees";
            CreateEmployee employee = new()
            {
                FirstName = "Joseph",
                LastName = "Doe",
                DepartmentId = 5,
                Email = "Joseph.Doe@fakecompany.com",
                Gender = 'M',
                ManagerId = 4,
                Salary = 5001.01M
            };
            StringContent jsonContent = new(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PostAsync(createUri, jsonContent);

            // Assert
            string stringResponse = await response.Content.ReadAsStringAsync();
            EmployeeDto created = JsonSerializer.Deserialize<EmployeeDto>(stringResponse);

            response.EnsureSuccessStatusCode();
            Assert.Equal(StatusCodes.Status201Created, (int)response.StatusCode);
            Assert.NotNull(response.Headers.Location);
            Assert.NotNull(created.Id);
            Assert.Equal(employee.Email, created.Email);
        }

        [Theory]
        [MemberData(nameof(GenerateInvalidUpdateEmployeeObjects))]
        public async Task UpdateEndpoint_WhenPassedInvalidEmployeeObject_ReturnsBadRequest(int id, UpdateEmployee employee)
        {
            // Arrange
            string updateUri = $"/api/v1/Employees/${id}";
            StringContent jsonContent = new(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PutAsync(updateUri, jsonContent);

            // Assert
            string stringResponse = await response.Content.ReadAsStringAsync();
            ProblemDetails errorResponse = JsonSerializer.Deserialize<ProblemDetails>(stringResponse);

            Assert.NotNull(errorResponse.Status);
            Assert.NotNull(errorResponse.Title);
            Assert.Equal(StatusCodes.Status400BadRequest, errorResponse.Status);
            Assert.Equal("One or more validation errors occurred.", errorResponse.Title);
        }

        [Fact]
        public async Task UpdateEndpoint_WhenPassedInvalidEmployeeObject_ReturnsNotFound()
        {
            // Arrange
            int id = 91;
            string updateUri = $"/api/v1/Employees/{id}";
            UpdateEmployee employee = new()
            {
                FirstName = "John",
                LastName = "Doe",
                DepartmentId = 5,
                Gender = 'M',
                ManagerId = null,
                Salary = 5001.01M
            };
            StringContent jsonContent = new(JsonSerializer.Serialize(employee), Encoding.UTF8, "application/json");

            // Act
            HttpResponseMessage response = await _client.PutAsync(updateUri, jsonContent);

            // Assert
            string stringResponse = await response.Content.ReadAsStringAsync();
            IDictionary<string, object> errorResponse = JsonSerializer.Deserialize<IDictionary<string, object>>(stringResponse);

            Assert.NotNull(errorResponse["StatusCode"]);
            Assert.NotNull(errorResponse["Message"]);
            Assert.Equal(StatusCodes.Status404NotFound, Convert.ToInt32(errorResponse["StatusCode"].ToString()));
            Assert.Equal("Resource not found", errorResponse["Message"].ToString());
        }

        [Theory]
        [InlineData(int.MinValue)]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(int.MaxValue)]
        public async Task DeleteEndpoint_WhenPassedInvalidEmployeeIds_ReturnsNotFound(int id)
        {
            // Arrange
            string deleteUri = $"/api/v1/Employees/{id}";

            // Act
            HttpResponseMessage response = await _client.DeleteAsync(deleteUri);

            // Assert
            string stringResponse = await response.Content.ReadAsStringAsync();
            IDictionary<string, object> errorResponse = JsonSerializer.Deserialize<IDictionary<string, object>>(stringResponse);

            Assert.NotNull(errorResponse["StatusCode"]);
            Assert.NotNull(errorResponse["Message"]);
            Assert.Equal(StatusCodes.Status404NotFound, Convert.ToInt32(errorResponse["StatusCode"].ToString()));
            Assert.Equal("Resource not found", errorResponse["Message"].ToString());
        }

        [Theory]
        [InlineData(4)]
        public async Task DeleteEndpoint_WhenPassedValidEmployeeId_ReturnsSuccess(int id)
        {
            // Arrange
            string deleteUri = $"/api/v1/Employees/{id}";

            // Act
            HttpResponseMessage response = await _client.DeleteAsync(deleteUri);

            // Assert
            Assert.Equal(StatusCodes.Status204NoContent, (int)response.StatusCode);
        }

        #region Test data
        private static IEnumerable<object[]> GenerateInvalidCreateEmployeeObjects()
        {
            yield return new object[] { default(CreateEmployee) };
            yield return new object[] 
            { 
                new CreateEmployee 
                {
                    FirstName = null,
                    LastName = "Doe",
                    DepartmentId = 5,
                    Email = "",
                    Gender = 'M',
                    ManagerId = 4,
                    Salary = 5001.01M
                }
            };

            yield return new object[]
            {
                new CreateEmployee
                {
                    FirstName = "John",
                    LastName = null,
                    DepartmentId = 5,
                    Email = "",
                    Gender = 'M',
                    ManagerId = 4,
                    Salary = 5001.01M
                }
            };

            yield return new object[]
            {
                new CreateEmployee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DepartmentId = 5,
                    Email = "",
                    Gender = 'M',
                    ManagerId = 4,
                    Salary = 5001.01M
                }
            };

            yield return new object[]
            {
                new CreateEmployee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DepartmentId = 5,
                    Email = "",
                    Gender = '\0',
                    ManagerId = 4,
                    Salary = 5001.01M
                }
            };
        }

        private static IEnumerable<object[]> GenerateInvalidUpdateEmployeeObjects()
        {
            yield return new object[] { int.MinValue, default(UpdateEmployee) };
            yield return new object[]
            {
                int.MaxValue,
                new UpdateEmployee
                {
                    FirstName = null,
                    LastName = "Doe",
                    DepartmentId = 5,
                    Gender = 'M',
                    ManagerId = 4,
                    Salary = 5001.01M
                }
            };

            yield return new object[]
            {
                0,
                new UpdateEmployee
                {
                    FirstName = "John",
                    LastName = null,
                    DepartmentId = 5,
                    Gender = 'M',
                    ManagerId = 4,
                    Salary = 5001.01M
                }
            };

            yield return new object[]
            {
                -1,
                new UpdateEmployee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DepartmentId = 5,
                    Gender = 'M',
                    ManagerId = 4,
                    Salary = 5001.01M
                }
            };

            yield return new object[]
            {
                -2,
                new UpdateEmployee
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DepartmentId = 5,
                    Gender = '\0',
                    ManagerId = 4,
                    Salary = 5001.01M
                }
            };
        }
        #endregion
    }
}
