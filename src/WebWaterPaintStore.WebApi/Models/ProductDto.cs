using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.WebApi.Models
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string UrlSlug { get; set; }
        public string Meta { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Actived { get; set; }
        public string ImageUrl { get; set; }
        public CategoryDto Category { get; set; }
        public IList<UnitDetailDto> UnitDetails { get; set; }
       


    }
}
