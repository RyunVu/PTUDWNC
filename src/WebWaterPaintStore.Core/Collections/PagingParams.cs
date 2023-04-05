using WebWaterPaintStore.Core.Contracts;

namespace WebWaterPaintStore.Core.Collections {
    public class PagingParams : IPagingParams {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
        public string SortColumn { get; set; } = "Id";
        public string SortOrder { get; set; } = "ASC";
    }
}
