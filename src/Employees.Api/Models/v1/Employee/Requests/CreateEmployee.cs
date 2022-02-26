using System.Text.Json.Serialization;

namespace Employees.Api.Models.v1.Employee.Requests
{
    public class CreateEmployee
    {
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("gender")]
        public char Gender { get; set; }

        [JsonPropertyName("salary")]
        public decimal Salary { get; set; }

        [JsonPropertyName("manager_id")]
        public int? ManagerId { get; set; }

        [JsonPropertyName("department_id")]
        public int DepartmentId { get; set; }
    }
}
