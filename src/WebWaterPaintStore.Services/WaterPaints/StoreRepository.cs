using Microsoft.EntityFrameworkCore;
using WebWaterPaintStore.Core.Contracts;
using WebWaterPaintStore.Core.Entities;
using WebWaterPaintStore.Data.Contexts;
using WebWaterPaintStore.Services.Extensions;

namespace WebWaterPaintStore.Services.WaterPaints
{
    public class StoreRepository : IStoreRepository
    {
        private readonly StoreDbContext _dbContext;

        public StoreRepository(StoreDbContext dbcontext)
        {
            _dbContext = dbcontext;
        }

        #region Product

        public async Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Product>()
                .Include(c => c.Category)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Product> GetProductBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Product>()
               .Include(c => c.Category)
               .FirstOrDefaultAsync(s => s.UrlSlug == slug, cancellationToken);
        }

        private IQueryable<Product> FilterProduct(IProductQuery productQuery)
        {
            var products = _dbContext.Set<Product>()
                .Include(s => s.Category)
                .WhereIf(productQuery.Year > 0, s => s.CreatedDate.Year == productQuery.Year)
                .WhereIf(productQuery.Month > 0, s => s.CreatedDate.Month == productQuery.Month)
                .WhereIf(productQuery.Day > 0, s => s.CreatedDate.Day == productQuery.Day)
                .WhereIf(!string.IsNullOrEmpty(productQuery.CategorySlug), s => s.Category.UrlSlug.Contains(productQuery.CategorySlug))
                .WhereIf(!string.IsNullOrEmpty(productQuery.ProductSlug), s => s.UrlSlug.Contains(productQuery.ProductSlug))
                .WhereIf(!string.IsNullOrEmpty(productQuery.Keyword), s =>
                    s.Name.Contains(productQuery.Keyword) ||
                    s.ShortDescription.Contains(productQuery.Keyword) ||
                    s.UrlSlug.Contains(productQuery.Keyword));
            return products;
        }

        public Task<IPagedList<Product>> GetPagedProductsAsync(IProductQuery productQuery, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            return FilterProduct(productQuery).ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<T>> GetPagedProductsAsync<T>(IProductQuery productQuery, IPagingParams pagingParams, Func<IQueryable<Product>, IQueryable<T>> mapper)
        {
            var products = FilterProduct(productQuery);
            var projectedProducts = mapper(products);

            return await projectedProducts.ToPagedListAsync(pagingParams);
        }


        #endregion

    }
}
