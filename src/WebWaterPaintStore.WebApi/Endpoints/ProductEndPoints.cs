using Mapster;
using MapsterMapper;
using Microsoft.EntityFrameworkCore.Metadata;
using WebWaterPaintStore.Core.Collections;
using WebWaterPaintStore.Core.DTO;
using WebWaterPaintStore.Services.WaterPaints;
using WebWaterPaintStore.WebApi.Models;

namespace WebWaterPaintStore.WebApi.Endpoints
{
    public static class ProductEndPoints{
        public static WebApplication MapProductEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/products");

            routeGroupBuilder.MapGet("/", GetProducts)
               .WithName("GetProducts")
               .Produces<ApiResponse<PaginationResult<ProductDto>>>();

            return app;
        }

        private static async Task<IResult> GetProducts(
            [AsParameters] ProductQuery query,
            [AsParameters] PagingModel pagingModel,
            IStoreRepository storeRepo)
        {
            var productsList = await storeRepo.GetPagedProductsAsync(
                query,
                pagingModel,
                p => p.ProjectToType<ProductDto>());

            var paginationResult = new PaginationResult<ProductDto>(productsList);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }

    }
}
