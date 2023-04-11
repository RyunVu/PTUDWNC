using WebWaterPaintStore.Core.Contracts;

namespace WebWaterPaintStore.Core.Collections
{
    public class CategoryQuery : ICategoryQuery
    {
        public string Keyword { get ; set ; }
        public bool Actived { get ; set ; }
    }
}
