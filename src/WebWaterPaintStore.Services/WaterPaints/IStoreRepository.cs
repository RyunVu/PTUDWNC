using WebWaterPaintStore.Core.Contracts;
using WebWaterPaintStore.Core.DTO;
using WebWaterPaintStore.Core.Entities;

namespace WebWaterPaintStore.Services.WaterPaints
{
    public interface IStoreRepository 
    {
        Task<Product> GetProductByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<Product> GetProductBySlugAsync(string slug,
            CancellationToken cancellationToken = default);

        Task<IPagedList<Product>> GetPagedProductsAsync(
            IProductQuery productQuery,
            IPagingParams pagingParams,
            CancellationToken cancellationToken = default);

        Task<IPagedList<T>> GetPagedProductsAsync<T>(
            IProductQuery productQuery,
            IPagingParams pagingParams,
            Func<IQueryable<Product>, IQueryable<T>> mapper);

    }
}
