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

        #endregion

    }
}
