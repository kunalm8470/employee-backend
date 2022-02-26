using System.Text.Json.Serialization;

namespace Employees.Domain.Models
{
    public class PaginatedList<T> where T : class
    {
        [JsonPropertyName("page")]
        public int PageIndex { get; set; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("total")]
        public int TotalItems { get; set; }

        [JsonPropertyName("data")]
        public List<T> Data { get; set; }

        public PaginatedList(IEnumerable<T> data, int totalItems, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            TotalItems = totalItems;
            Data = new List<T>(pageSize);
            Data.AddRange(data);
        }

        public PaginatedList()
        {

        }
    }
}
