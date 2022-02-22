using System.Text.Json.Serialization;

namespace Employees.Api.Models.v1.Employee.Requests
{
    public class ListEmployee
    {
        [JsonPropertyName("page")]
        public int Page { get; set; } = 1;

        [JsonPropertyName("limit")]
        public int Limit { get; set; } = 10;
    }
}
