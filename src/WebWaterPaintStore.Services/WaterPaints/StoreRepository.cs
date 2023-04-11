using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using WebWaterPaintStore.Core.Contracts;
using WebWaterPaintStore.Core.DTO;
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

        public Task<IPagedList<Product>> GetPagedProductQueryAsync(IProductQuery productQuery, IPagingParams pagingParams, CancellationToken cancellationToken = default)
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

        #region Category

        public async Task<Category> GetCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Category>()
                .Where(c => c.Id.Equals(id))
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<Category> GetCategoryBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Category>()
                .Where(c => c.UrlSlug.Equals(slug))
                .FirstOrDefaultAsync(cancellationToken);
        }

        private IQueryable<CategoryItem> CategoriesFilter(ICategoryQuery cateQuery)
        {
            var categories = _dbContext.Set<Category>()
                .WhereIf(!string.IsNullOrWhiteSpace(cateQuery.Keyword), s => 
                s.Name.Contains(cateQuery.Keyword) ||
                s.UrlSlug.Contains(cateQuery.Keyword) ||
                s.Description.Contains(cateQuery.Keyword))
                .Select(s => new CategoryItem(){
                    Id = s.Id,
                    Name = s.Name,
                    UrlSlug = s.UrlSlug,
                    Description = s.Description,
                    ProductsCount = s.Products.Count(p => p.Actived)
                });
            return categories;
        }


        public async Task<IPagedList<CategoryItem>> GetPagedCategoryQueryAsync(ICategoryQuery cateQuery, IPagingParams pagingParams, CancellationToken cancellationToken)
        {
            return await CategoriesFilter(cateQuery).ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<CategoryItem>> GetPagedCategoriesAsync(IPagingParams pagingParams, string name = null, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Category>()
                .AsNoTracking()
                .WhereIf(!string.IsNullOrWhiteSpace(name), x => x.Name.Contains(name))
                .Select(a => new CategoryItem()
                {
                    Id = a.Id,
                    Name = a.Name,
                    UrlSlug = a.UrlSlug,
                    Description = a.Description,
                    ProductsCount = a.Products.Count(p => p.Actived)
                })
                .ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<T>> GetPagedCategoriesAsync<T>(Func<IQueryable<Category>, IQueryable<T>> mapper, IPagingParams pagingParams, string keyword = null, CancellationToken cancellationToken = default)
        {
            var cateQuery = _dbContext.Set<Category>().AsNoTracking();

            if (!string.IsNullOrEmpty(keyword))
            {
                cateQuery = cateQuery.Where(x => x.Name.Contains(keyword));
            }

            return await mapper(cateQuery)
                .ToPagedListAsync(pagingParams, cancellationToken);
        }



        #endregion

    }
}
