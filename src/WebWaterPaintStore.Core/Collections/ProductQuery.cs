using WebWaterPaintStore.Core.Contracts;

namespace WebWaterPaintStore.Core.Collections
{
    public class ProductQuery : IProductQuery
    {
        public int? CategoryId { get;set; }
        public string CategorySlug { get; set; } = "";
        public string ProductSlug { get; set; } = "";
        public int? Year { get; set; } = 0;
        public int? Month { get; set; } = 0;
        public int? Day { get; set; } = 0;
        public string Keyword { get; set; } = "";
    }
}
