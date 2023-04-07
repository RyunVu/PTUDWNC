using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.WebApi.Models
{
    public class ProductEditModel
    {
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Meta { get; set; }
        public string UrlSlug { get; set; }
        
        public int CategoryId { get; set; }
        public IList<UnitDetail> ProductUnitDetails { get; set; }

        public ProductEditModel()
        {
            ProductUnitDetails = new List<UnitDetail>();
        }
    }
}
