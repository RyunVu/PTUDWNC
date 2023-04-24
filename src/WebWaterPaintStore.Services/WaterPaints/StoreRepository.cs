using Azure;
using Microsoft.EntityFrameworkCore;
using SlugGenerator;
using System.ComponentModel;
using WebWaterPaintStore.Core.Collections;
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
                .Include(u => u.UnitDetails)
                .FirstOrDefaultAsync(s => s.Id == id, cancellationToken);
        }

        public async Task<Product> GetProductBySlugAsync(string slug, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Product>()
               .Include(c => c.Category)
                .Include(u => u.UnitDetails)
               .FirstOrDefaultAsync(s => s.UrlSlug.Equals(slug), cancellationToken);
        }


        private IQueryable<Product> FilterProduct(IProductQuery productQuery)
        {
            var products = _dbContext.Set<Product>()
                .Include(s => s.Category)
                .Include(u => u.UnitDetails)
                .WhereIf(productQuery.Year > 0, s => s.CreatedDate.Year == productQuery.Year)
                .WhereIf(productQuery.Month > 0, s => s.CreatedDate.Month == productQuery.Month)
                .WhereIf(productQuery.Day > 0, s => s.CreatedDate.Day == productQuery.Day)
                .WhereIf(!string.IsNullOrEmpty(productQuery.CategorySlug), s => s.Category.UrlSlug.Contains(productQuery.CategorySlug))
                .WhereIf(productQuery.CategoryId > 0, s=> s.Category.Id.Equals(productQuery.CategoryId))
                .WhereIf(!string.IsNullOrEmpty(productQuery.ProductSlug), s => s.UrlSlug.Contains(productQuery.ProductSlug))
                .WhereIf(!string.IsNullOrWhiteSpace(productQuery.UnitTag), p => p.UnitDetails.Any(t => t.UnitTag == productQuery.UnitTag))
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

        public async Task ToggleProductActivedStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<Product>().Where(x => x.Id.Equals(id)).ExecuteUpdateAsync(p => p.SetProperty(x => x.Actived, x => !x.Actived), cancellationToken);
        }

        public async Task<IList<Product>> GetRandomsProductAsync(int nums, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Product>()
                .Include(c => c.Category)
                .Include(u => u.UnitDetails)
                .OrderBy(p => Guid.NewGuid())
                .Take(nums)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> IsProductSlugExistedAsync(int productId, string slug, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Product>()
                .AnyAsync(p => p.Id != productId && p.UrlSlug == slug, cancellationToken);
        }

        public async Task<bool> DeleteProductByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Product>()
                .Where(p => p.Id.Equals(id))
                .ExecuteDeleteAsync(cancellationToken) > 0;
        }

        public async Task<bool> AddOrUpdateProductAsync(Product product, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Set<Product>().Any(p => p.Id.Equals(product.Id)))
            {
                _dbContext.Entry(product).State = EntityState.Modified;
            }
            else
            {
                _dbContext.Products.Add(product);
            }
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task<bool> SetImageUrlAsync(
        int id, string imageUrl,
        CancellationToken cancellationToken = default)
        {
            return await _dbContext.Products
                .Where(x => x.Id == id)
                .ExecuteUpdateAsync(x =>
                    x.SetProperty(a => a.ImageUrl, a => imageUrl),
                    cancellationToken) > 0;
        }

        public async Task<IList<Product>> GetProductsByUnitTagAsync(
            string slug,
            CancellationToken cancellationToken = default)
        {
            
            return await _dbContext.Set<Product>()
                .Include(c => c.Category)
                .Include(u => u.UnitDetails)
                .Where(p => p.UnitDetails.Any(t => t.UnitTag == slug))
                .ToListAsync(cancellationToken); 
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
                    Actived = a.Actived,
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

        public async Task<bool> IsCategorySlugExistedAsync(int categoryId, string slug, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Category>()
                .AnyAsync(c => c.Id != categoryId && c.UrlSlug == slug, cancellationToken);
        }

        public async Task<bool> DeleteCategoryByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Category>()
                .Where(c => c.Id.Equals(id))
                .ExecuteDeleteAsync(cancellationToken) > 0;
        }

        public async Task<bool> AddOrUpdateCategoryAsync(Category category, CancellationToken cancellationToken)
        {
            if (_dbContext.Set<Category>().Any(c => c.Id.Equals(category.Id)))
            {
                _dbContext.Entry(category).State = EntityState.Modified;
            }
            else
            {
                _dbContext.Categories.Add(category);
            }
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        #endregion

        #region UnitDetail
        public async Task<UnitDetail> GetUnitByIdAsync(int productId,int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UnitDetail>()
                .Include(c => c.Product)
                .FirstOrDefaultAsync(s => s.Id.Equals(id) && s.Product.Id.Equals(productId), cancellationToken);
        }

        public async Task<UnitDetail> GetUnitByTagAsync(int productId, string tag, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UnitDetail>()
               .Include(c => c.Product)
               .FirstOrDefaultAsync(s => s.UnitTag.Equals(tag) && s.ProductId.Equals(productId), cancellationToken);
        }
        private IQueryable<UnitDetail> FilterUnit(IUnitQuery unitQuery)
        {
            int keyNumber = 0;
            var keyword = !string.IsNullOrWhiteSpace(unitQuery.Keyword) ? unitQuery.Keyword.ToLower() : "";
            int.TryParse(unitQuery.Keyword, out keyNumber);

            var units = _dbContext.Set<UnitDetail>()
                .Include(p => p.Product)
                .WhereIf(unitQuery.Actived, s => s.Actived)
                .WhereIf(unitQuery.NotActived, s => !s.Actived)
                .WhereIf(!string.IsNullOrEmpty(unitQuery.ProductSlug), s => s.Product.UrlSlug.ToLower().Contains(unitQuery.ProductSlug.ToLower()))
                .WhereIf(!string.IsNullOrEmpty(unitQuery.Keyword), s =>
                    s.UnitTag.ToLower().Contains(unitQuery.Keyword.ToLower()));
            return units;
        }

        public Task<IPagedList<UnitDetail>> GetPagedUnitQueryAsync(IUnitQuery unitQuery, IPagingParams pagingParams, CancellationToken cancellationToken = default)
        {
            return FilterUnit(unitQuery).ToPagedListAsync(pagingParams, cancellationToken);
        }

        public async Task<IPagedList<T>> GetPagedUnitsAsync<T>(IUnitQuery unitQuery, IPagingParams pagingParams, Func<IQueryable<UnitDetail>, IQueryable<T>> mapper)
        {
            var units = FilterUnit(unitQuery);
            var projectedUnits = mapper(units);

            return await projectedUnits.ToPagedListAsync(pagingParams);
        }

        public async Task<bool> IsUnitTagExistedAsync(int postId, string tag, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<Product>()
                .Include(u => u.UnitDetails)
                .AnyAsync(p => p.Id.Equals(postId) &&  p.UnitDetails.Any(u => u.UnitTag.Equals(tag)));
        }

        public async Task<bool> DeleteUnitByIdAsync(int id, CancellationToken cancellationToken = default)
        {
            return await _dbContext.Set<UnitDetail>()
                .Where(p => p.Id.Equals(id))
                .ExecuteDeleteAsync(cancellationToken) > 0;
        }

        public async Task<bool> AddOrUpdateUnitAsync(UnitDetail unit, CancellationToken cancellationToken = default)
        {
            if (_dbContext.Set<UnitDetail>().Any(p => p.Id.Equals(unit.Id)))
            {
                _dbContext.Entry(unit).State = EntityState.Modified;
            }
            else
            {
                _dbContext.UnitDetails.Add(unit);
            }
            return await _dbContext.SaveChangesAsync(cancellationToken) > 0;
        }

        public async Task ToggleProductUnitActivedStatusAsync(int id, CancellationToken cancellationToken = default)
        {
            await _dbContext.Set<UnitDetail>().Where(x => x.Id.Equals(id)).ExecuteUpdateAsync(p => p.SetProperty(x => x.Actived, x => !x.Actived), cancellationToken);
        }
        #endregion
    }
}
