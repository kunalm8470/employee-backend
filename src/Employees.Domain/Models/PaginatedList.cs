using System.Text.Json.Serialization;

namespace Employees.Domain.Models
{
    public class PaginatedList<T> where T : class
    {
        [JsonPropertyName("page")]
        public int PageIndex { get; }

        [JsonPropertyName("page_size")]
        public int PageSize { get; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; }

        [JsonPropertyName("total")]
        public int TotalItems { get; }

        [JsonPropertyName("data")]
        public List<T> Data { get; }

        public PaginatedList(IEnumerable<T> data, int totalItems, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            PageSize = pageSize;
            TotalPages = (int)Math.Ceiling(totalItems / (double)pageSize);
            TotalItems = totalItems;
            Data = new List<T>(pageSize);
            Data.AddRange(data);
        }

        // This is for serialization.
        private PaginatedList()
        {
        }
    }
}
