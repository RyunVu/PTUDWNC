using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebWaterPaintStore.Core.Collections;
using WebWaterPaintStore.Core.DTO;
using WebWaterPaintStore.Core.Entities;
using WebWaterPaintStore.Services.WaterPaints;
using WebWaterPaintStore.WebApi.Filters;
using WebWaterPaintStore.WebApi.Media;
using WebWaterPaintStore.WebApi.Models;

namespace WebWaterPaintStore.WebApi.Endpoints
{
    public static class UnitEndPoints
    {
        public static WebApplication MapUnitEndpoints(
           this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/product/unit");

            routeGroupBuilder.MapGet("/", GetProductUnits)
               .WithName("GetProductUnits")
               .Produces<ApiResponse<PaginationResult<UnitDetailDto>>>();

            routeGroupBuilder.MapPost("/", AddProductUnit)
               .WithName("AddProductUnit")
               .AddEndpointFilter<ValidatorFilter<UnitEditModel>>()
               .Produces(201)
               .Produces(400)
               .Produces(409);

            routeGroupBuilder.MapPut("/{id:int}", UpdateProductUnit)
                .WithName("UpdateProductUnit")
                .AddEndpointFilter<ValidatorFilter<UnitEditModel>>()
                .Produces(204)
                .Produces(400)
                .Produces(409);

            //routeGroupBuilder.MapGet("/ToggleActive/{id:int}", ToggleActiveStatus)
            //    .WithName("ToggleUnitActiveStatus")
            //    .Produces(204)
            //    .Produces(404);

            routeGroupBuilder.MapDelete("/{id:int}", DeleteProductUnit)
                .WithName("DeleteProductUnit")
                .Produces(204)
                .Produces(404);

            return app;
        }
        private static async Task<IResult> GetProductUnits(
            [AsParameters] UnitQuery query,
            [AsParameters] PagingModel pagingModel,
            IStoreRepository storeRepo)
        {
            var unitsList = await storeRepo.GetPagedUnitsAsync(
                query,
                pagingModel,
                p => p.ProjectToType<UnitDetailDto>());

            var paginationResult = new PaginationResult<UnitDetailDto>(unitsList);
            return Results.Ok(ApiResponse.Success(paginationResult));
        }

        private static async Task<IResult> AddProductUnit(
            UnitEditModel model,
            IStoreRepository storeRepo,
            IMapper mapper)
        {
            var isExitstProduct = await storeRepo.GetProductByIdAsync(model.ProductId);

            if (isExitstProduct == null)
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.NotFound,
                    $"Sản phẩm không tồn tại!"));
            }

            if (await storeRepo.IsUnitTagExistedAsync(isExitstProduct.Id, model.UnitTag))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
                    $"Tag '{model.UnitTag}' đã được sử dụng"));
            }     

            var unit = mapper.Map<UnitDetail>(model);

            await storeRepo.AddOrUpdateUnitAsync(unit);

            return Results.Ok(ApiResponse.Success(mapper.Map<UnitDetailItem>(unit), HttpStatusCode.Created));
        }

        private static async Task<IResult> UpdateProductUnit(
            int id,
            UnitEditModel model,
            IStoreRepository storeRepo,
            IMapper mapper)
        {
            var isExitsProduct = await storeRepo.GetProductByIdAsync(model.ProductId);

            if (isExitsProduct == null)
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.NotFound,
                    $"Sản phẩm không tồn tại!"));
            }

            var unit = await storeRepo.GetUnitByIdAsync(isExitsProduct.Id, id);

            if (unit == null)
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.NotFound,
                    $"Không tìm thấy loại sản phẩm với id: `{id}`"));
            }       

            if (await storeRepo.IsUnitTagExistedAsync(isExitsProduct.Id, model.UnitTag))
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.Conflict,
                    $"Slug {model.UnitTag} đã được sử dụng"));
            }

            mapper.Map(model, unit);
            unit.Id = id;
            unit.Product = null;

            return await storeRepo.AddOrUpdateUnitAsync(unit)
                ? Results.Ok(ApiResponse.Success("Unit is updated", HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Không tìm thấy sản phẩm"));
        }

        //private static async Task<IResult> ToggleActiveStatus(
        //    int id,
        //    IStoreRepository storeRepo)
        //{
        //    var oldUnit = await storeRepo.GetUnitByIdAsync(id);

        //    if (oldUnit == null)
        //    {
        //        return Results.Ok(ApiResponse.Fail(
        //            HttpStatusCode.NotFound,
        //            $"Không tìm thấy loại sản phẩm với id: `{id}`"));
        //    }

        //    await storeRepo.ToggleProductUnitActivedStatusAsync(id);
        //    return Results.Ok(ApiResponse.Success("Toggle product active success"));
        //}

        private static async Task<IResult> DeleteProductUnit(
            int id,
            IStoreRepository storeRepo)
        {
            var oldProduct = await storeRepo.GetProductByIdAsync(id);

            return await storeRepo.DeleteUnitByIdAsync(id)
                ? Results.Ok(ApiResponse.Success(HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.NotFound,
                    $"Không tìm thấy loại sản phẩm với id: `{id}`"));

        }

        private static async Task<IResult> GetProductByUnitTag(
            string tag,
            IMapper mapper,
            IStoreRepository storeRepo)
        {
            var products = await storeRepo.GetProductsByUnitTagAsync(tag);

            var productDto = mapper.Map<ProductDto>(products);

            return Results.Ok(ApiResponse.Success(productDto));
        }
    }
}
