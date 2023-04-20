namespace WebWaterPaintStore.WebApi.Models
{
    public class CategoryFilterModel : PagingModel
    {
        public string Keyword { get; set; }
        public bool Actived { get; set; }
    }
}
