using System.Text.Json.Serialization;

namespace Employees.Api.Models.v1.Employee.Responses
{
    public class EmployeeDepartment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
