using System.Threading.Tasks;
using WebWaterPaintStore.Core.Contracts;
using WebWaterPaintStore.Core.DTO;
using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.Services.WaterPaints
{
    public interface IStoreRepository 
    {
        #region Product
            Task<Product> GetProductByIdAsync(
                int id,
                CancellationToken cancellationToken = default);

            Task<Product> GetProductBySlugAsync(string slug,
                CancellationToken cancellationToken = default);

            Task<IPagedList<Product>> GetPagedProductQueryAsync(
                IProductQuery productQuery,
                IPagingParams pagingParams,
                CancellationToken cancellationToken = default);

            Task<IPagedList<T>> GetPagedProductsAsync<T>(
                IProductQuery productQuery,
                IPagingParams pagingParams,
                Func<IQueryable<Product>, IQueryable<T>> mapper);

            Task<IList<Product>> GetRandomsProductAsync(int nums, CancellationToken cancellationToken = default);

            Task<bool> IsProductSlugExistedAsync(int productId, string slug, CancellationToken cancellationToken = default);

            Task<bool> DeleteProductByIdAsync(int id, CancellationToken cancellationToken = default);
            
            Task<bool> AddOrUpdateProductAsync(Product product, CancellationToken cancellationToken = default);

            Task ToggleProductActivedStatusAsync(int id, CancellationToken cancellationToken = default);
            Task<bool> SetImageUrlAsync(int id, string imageUrl, CancellationToken cancellationToken = default);
            Task<IList<Product>> GetProductsByUnitTagAsync(string slug, CancellationToken cancellationToken = default);

        #endregion

        #region Category

        Task<Category> GetCategoryByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Category> GetCategoryBySlugAsync(string slug,
            CancellationToken cancellationToken = default);

        Task<IPagedList<CategoryItem>> GetPagedCategoryQueryAsync(
            ICategoryQuery cateQuery,
            IPagingParams pagingParams,
            CancellationToken cancellationToken);

        Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(
            IPagingParams pagingParams,
            string name = null,
            CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetPagedCategoriesAsync<T>(
            Func<IQueryable<Category>, IQueryable<T>> mapper,
            IPagingParams pagingParams,
            string keyword = null,
            CancellationToken cancellationToken = default);

        Task<bool> IsCategorySlugExistedAsync(int categoryId, string slug, CancellationToken cancellationToken = default);

        Task<bool> DeleteCategoryByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateCategoryAsync(Category category, CancellationToken cancellationToken = default);
        #endregion

        #region UnitDetail
        Task<UnitDetail> GetUnitByIdAsync(
            int productId,
            int id,
            CancellationToken cancellationToken = default);

        Task<UnitDetail> GetUnitByTagAsync(
            int productId, 
            string tag,
            CancellationToken cancellationToken = default);

        Task<IPagedList<UnitDetail>> GetPagedUnitQueryAsync(
            IUnitQuery unitQuery,
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetPagedUnitsAsync<T>(
            IUnitQuery unitQuery,
            IPagingParams pagingParams,
            Func<IQueryable<UnitDetail>, IQueryable<T>> mapper);

        Task<bool> IsUnitTagExistedAsync(int postId, string tag, CancellationToken cancellationToken = default);

        Task<bool> DeleteUnitByIdAsync(int id, CancellationToken cancellationToken = default);

        Task<bool> AddOrUpdateUnitAsync(UnitDetail unit, CancellationToken cancellationToken = default);

        Task ToggleProductUnitActivedStatusAsync(int id, CancellationToken cancellationToken = default);

        Task<UnitDetail> GetUnitByIdAsync(int id, CancellationToken cancellation = default);

        #endregion
    }
}
