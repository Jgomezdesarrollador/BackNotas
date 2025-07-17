namespace Application.DTOs.Paginacion
{
    public class PagedResultDto<T>
    {
        public IEnumerable<T> Items { get; set; } = [];
        public int TotalCount { get; set; }
    }
}
