using System.Text.Json.Serialization;

namespace Employees.Api.Models.v1.Employee.Responses
{
    public class EmployeeDto
    {
        [JsonPropertyName("code")]
        public Guid Code { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }

        [JsonPropertyName("gender")]
        public char GenderAbbreviation { get; set; }

        [JsonPropertyName("salary")]
        public decimal Salary { get; set; }

        [JsonPropertyName("department")]
        public string Department { get; set; }

        [JsonPropertyName("manager")]
        public EmployeeManager Manager { get; set; }

        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonPropertyName("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
