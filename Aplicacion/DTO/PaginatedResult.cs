using System.Text.Json.Serialization;

namespace Aplicacion.DTO
{
    public class PaginatedResult<T>
    {
        public List<T> Items { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
        public int TotalPages { get; set; }
        
        [JsonIgnore]
        public bool HasPreviousPage => PageNumber > 1;
        
        [JsonIgnore]
        public bool HasNextPage => PageNumber < TotalPages;

        public PaginatedResult(List<T> items, int pageNumber, int pageSize, int totalItems)
        {
            Items = items ?? new List<T>();
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalItems = totalItems;
            TotalPages = CalculateTotalPages(totalItems, pageSize);
        }

        private static int CalculateTotalPages(int totalItems, int pageSize)
        {
            if (pageSize <= 0) 
                return 0;
            return (int)Math.Ceiling(totalItems / (double)pageSize);
        }
    }
}