using System.Text.Json.Serialization;

namespace Employees.Api.Models.v1.Employee.Responses
{
    public class EmployeeManager
    {
        [JsonPropertyName("code")]
        public Guid Code { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("department")]
        public string? Department { get; set; }
    }
}
