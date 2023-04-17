using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebWaterPaintStore.Core.Collections;
using WebWaterPaintStore.Core.DTO;
using WebWaterPaintStore.Core.Entities;
using WebWaterPaintStore.Services.WaterPaints;
using WebWaterPaintStore.WebApi.Filters;
using WebWaterPaintStore.WebApi.Models;

namespace WebWaterPaintStore.WebApi.Endpoints
{
    public static class CategoryEndPoints
    {
        public static WebApplication MapCategoryEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/category");

            routeGroupBuilder.MapGet("/", GetCategories)
                .WithName("GetCategories")
                .Produces<ApiResponse<PaginationResult<CategoryItem>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetCategoryDetail)
               .WithName("GetCategoryById")
               .Produces<ApiResponse<CategoryItem>>()
               .Produces(404);

            routeGroupBuilder.MapGet("/{slug:regex(^[a-z0-9_-]+$)}/product", GetPostsByCategorySlug)
                .WithName("GetPostsByCategorySlug")
                .Produces<ApiResponse<PaginationResult<ProductDto>>>();

            routeGroupBuilder.MapPost("/", AddCategory)
                .WithName("AddNewCategory")
                .AddEndpointFilter<ValidatorFilter<CategoryEditModel>>()
                .Produces(201)
                .Produces(400)
                .Produces(409);

            routeGroupBuilder.MapPut("/{id:int}", UpdateCategory)
                .WithName("UpdateACategory")
                .AddEndpointFilter<ValidatorFilter<CategoryEditModel>>()
                .Produces(204)
                .Produces(400)
                .Produces(409);

            routeGroupBuilder.MapDelete("/{id:int}", DeleteCategory)
                .WithName("DeleteACategory")
                .Produces(204)
                .Produces(404);
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

        private static async Task<IResult> GetCategoryDetail(
           int id,
           IStoreRepository StoreRepo,
           IMapper mapper)
        {

            var category = await StoreRepo.GetCategoryByIdAsync(id);
            return category == null ? Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy chủ đề có mã số `{id}`"))
                                    : Results.Ok(ApiResponse.Success(mapper.Map<CategoryItem>(category)));

        }

        private static async Task<IResult> GetPostsByCategorySlug(
            [FromRoute] string slug,
            [AsParameters] PagingModel pagingModel,
            IStoreRepository StoreRepo)
        {

            var productQuery = new ProductQuery()
            {
                CategorySlug = slug
            };

            var productsList = await StoreRepo.GetPagedProductsAsync(
                productQuery, pagingModel,
                products => products.ProjectToType<ProductDto>());

            var paginationResult = new PaginationResult<ProductDto>(productsList);

            return Results.Ok(ApiResponse.Success(paginationResult));
        }


        private static async Task<IResult> AddCategory(
            CategoryEditModel model,
            IStoreRepository StoreRepo,
            IMapper mapper)
        {

            if (await StoreRepo.IsCategorySlugExistedAsync(0, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
                    $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }

            var category = mapper.Map<Category>(model);
            await StoreRepo.AddOrUpdateCategoryAsync(category);

            return Results.Ok(ApiResponse.Success(mapper.Map<CategoryItem>(category), HttpStatusCode.Created));
        }

        private static async Task<IResult> UpdateCategory(
            int id,
            CategoryEditModel model,
            IStoreRepository StoreRepo,
            IMapper mapper)
        {

            if (await StoreRepo.IsCategorySlugExistedAsync(id, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
                    $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }

            var category = mapper.Map<Category>(model);

            category.Id = id;

            return await StoreRepo.AddOrUpdateCategoryAsync(category)
                    ? Results.Ok(ApiResponse.Success("Chủ đề đã được cập nhập", HttpStatusCode.NoContent))
                    : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy chủ đề với id: `{id}`"));
        }

        private static async Task<IResult> DeleteCategory(int id,
            IStoreRepository StoreRepo)
        {
            return await StoreRepo.DeleteCategoryByIdAsync(id)
                    ? Results.Ok(ApiResponse.Success("Chủ đề đã được xóa", HttpStatusCode.NoContent))
                    : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy chủ đề với id: `{id}`"));
        }
    }    
}
