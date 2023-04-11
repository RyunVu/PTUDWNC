using WebWaterPaintStore.Core.Collections;
using WebWaterPaintStore.Core.DTO;
using WebWaterPaintStore.Services.WaterPaints;
using WebWaterPaintStore.WebApi.Models;

namespace WebWaterPaintStore.WebApi.Endpoints
{
    public static class CategoryEndPoints
    {
        public static WebApplication MapCategoryEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/categories");

            routeGroupBuilder.MapGet("/", GetCategories)
                .WithName("GetCategories")
                .Produces<ApiResponse<PaginationResult<CategoryItem>>>();

            return app;
        }

        private static async Task<IResult> GetCategories(
            [AsParameters] CategoryFilterModel model,
            IStoreRepository StoreRepo)
        {
            var categoriesList = await StoreRepo.GetPagedCategoriesAsync(model, model.Keyword);

            var paginationResult = new PaginationResult<CategoryItem>(categoriesList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }

    }    
}
