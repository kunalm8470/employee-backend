using System.Text.Json.Serialization;

namespace Employees.Api.Models.v1.Employee.Responses
{
    public class EmployeeManager
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("department")]
        public EmployeeDepartment Department { get; set; }
    }
}
