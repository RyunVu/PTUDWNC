using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
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
    public static class ProductEndPoints{
        public static WebApplication MapProductEndpoints(
            this WebApplication app)
        {
            var routeGroupBuilder = app.MapGroup("/api/product");

            #region Product

            routeGroupBuilder.MapGet("/", GetProducts)
              .WithName("GetProducts")
              .Produces<ApiResponse<PaginationResult<ProductDto>>>();

            routeGroupBuilder.MapGet("/random/{limit:int}", GetRandomProducts)
                .WithName("GetRandomProducts")
                .Produces<ApiResponse<PaginationResult<ProductDto>>>();

            routeGroupBuilder.MapGet("/{id:int}", GetProductById)
                .WithName("GetProductById")
                .Produces<ApiResponse<ProductDto>>()
                .Produces(404);

            routeGroupBuilder.MapGet("/byslug/{slug:regex(^[a-z0-9_-]+$)}", GetProductBySlug)
                .WithName("GetProductBySlug")
                .Produces<ApiResponse<PaginationResult<ProductDto>>>()
                .Produces(404);

            routeGroupBuilder.MapPost("/", AddProduct)
               .WithName("AddProduct")
               .AddEndpointFilter<ValidatorFilter<ProductEditModel>>()
               .Produces(201)
               .Produces(400)
               .Produces(409);

            routeGroupBuilder.MapPut("/{id:int}", UpdateProduct)
                .WithName("UpdateProduct")
                .AddEndpointFilter<ValidatorFilter<ProductEditModel>>()
                .Produces(204)
                .Produces(400)
                .Produces(409);

            routeGroupBuilder.MapPost("/toggleProduct/{id:int}", ToggleActiveStatus)
                .WithName("TogglePublicStatus")
                .Produces<ApiResponse<string>>();

            routeGroupBuilder.MapPost("/{id:int}/picture", SetProductPicture)
                .WithName("SetProductPicture")
                .Accepts<IFormFile>("multipart/form-data")
                .Produces<ApiResponse<string>>()
                .Produces(400);

            routeGroupBuilder.MapDelete("/{id:int}", DeleteProduct)
                .WithName("DeleteProduct")
                .Produces(204)
                .Produces(404);


            #endregion

            #region Unit

            routeGroupBuilder.MapGet("/{id:int}/unit/{unitId:int}", GetProductUnitById)
                .WithName("GetProductUnitById")
                .Produces<ApiResponse<UnitDetailItem>>()
                .Produces(404);

            routeGroupBuilder.MapGet("/{id:int}/unit/bytag/{tag::regex(^[a-z0-9_-]+$)}", GetProductUnitByTag)
                .WithName("GetProductUnitByTag")
                .Produces<ApiResponse<PaginationResult<UnitDetailItem>>>()
                .Produces(404);

            #endregion

            return app;
        }


        #region Product

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

        private static async Task<IResult> GetRandomProducts(
            int limit,
            IStoreRepository storeRepo,
            IMapper mapper)
        {
            var productsList = await storeRepo.GetRandomsProductAsync(limit);
            var productDto = mapper.Map<ProductDto>(productsList);

            return Results.Ok(ApiResponse.Success(productDto));
        }
        private static async Task<IResult> GetProductById(
            int id,
            IStoreRepository storeRepo,
            IMapper mapper)
        {
            var product = await storeRepo.GetProductByIdAsync(id);

            var productDetail = mapper.Map<ProductDto>(product);

            return productDetail != null
                    ? Results.Ok(ApiResponse.Success(productDetail))
                    : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy sản phẩm với id: `{id}`"));
        }

        private static async Task<IResult> GetProductBySlug(
            [FromRoute] string slug,
            IStoreRepository storeRepo,
            IMapper mapper)
        {
            var products = await storeRepo.GetProductBySlugAsync(slug);

            var productDetail = mapper.Map<ProductDto>(products);
            return productDetail != null
                ? Results.Ok(ApiResponse.Success(productDetail))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy sản phẩm với mã định danh: `{slug}`"));
        }

        private static async Task<IResult> AddProduct(
            ProductEditModel model,
            IStoreRepository storeRepo,
            IMapper mapper)
        {
            if (await storeRepo.IsProductSlugExistedAsync(0, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(HttpStatusCode.Conflict,
                    $"Slug '{model.UrlSlug}' đã được sử dụng"));
            }

            var isExitsCategory = await storeRepo.GetCategoryByIdAsync(model.CategoryId);

            if (isExitsCategory == null)
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.NotFound,
                    $"Mã chủ đề không tồn tại!"));
            }

            var product = mapper.Map<Product>(model);
            product.CreatedDate = DateTime.Now;

            await storeRepo.AddOrUpdateProductAsync(product);

            return Results.Ok(ApiResponse.Success(mapper.Map<ProductDetail>(product), HttpStatusCode.Created));
        }

        private static async Task<IResult> UpdateProduct(
            int id,
            ProductEditModel model,
            IStoreRepository storeRepo,
            IMapper mapper)
        {
            var product = await storeRepo.GetProductByIdAsync(id);

            if (product == null)
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.NotFound,
                    $"Không tìm thấy sản phẩm với id: `{id}`"));
            }

            if (await storeRepo.IsProductSlugExistedAsync(id, model.UrlSlug))
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.Conflict,
                    $"Slug {model.UrlSlug} đã được sử dụng"));
            }

            var isExitsCategory = await storeRepo.GetCategoryByIdAsync(model.CategoryId);

            if (isExitsCategory == null)
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.NotFound,
                    $"Mã chủ đề không tồn tại!"));
            }

            mapper.Map(model, product);
            product.Id = id;
            product.Category = null;

            return await storeRepo.AddOrUpdateProductAsync(product)
                ? Results.Ok(ApiResponse.Success("Product is updated", HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, "Không tìm thấy sản phẩm"));
        }

        private static async Task<IResult> ToggleActiveStatus(
            int id,
            IStoreRepository storeRepo)
        {
            var oldProduct = await storeRepo.GetProductByIdAsync(id);

            if (oldProduct == null)
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.NotFound,
                    $"Không tìm thấy sản phẩm với id: `{id}`"));
            }

            await storeRepo.ToggleProductActivedStatusAsync(id);
            return Results.Ok(ApiResponse.Success("Toggle product active success"));
        }

        private static async Task<IResult> SetProductPicture(
            int id, IFormFile imageFile,
            IStoreRepository storeRepo,
            IMediaManager mediaManager)
        {
            var oldProduct = await storeRepo.GetProductByIdAsync(id);
            if (oldProduct == null)
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.NotFound,
                    $"Không tìm thấy sản phẩm với id: `{id}`"));
            }

            await mediaManager.DeleteFileAsync(oldProduct.ImageUrl);

            var imageUrl = await mediaManager.SaveFileAsync(
                imageFile.OpenReadStream(),
                imageFile.FileName, imageFile.ContentType);

            if (string.IsNullOrWhiteSpace(imageUrl))
            {
                return Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.BadRequest,
                    "Không lưu được tệp"));
            }

            await storeRepo.SetImageUrlAsync(id, imageUrl);
            return Results.Ok(ApiResponse.Success(imageUrl));
        }

        private static async Task<IResult> DeleteProduct(
            int id,
            IStoreRepository storeRepo,
            IMediaManager _media)
        {
            var oldProduct = await storeRepo.GetProductByIdAsync(id);

            await _media.DeleteFileAsync(oldProduct.ImageUrl);

            return await storeRepo.DeleteProductByIdAsync(id)
                ? Results.Ok(ApiResponse.Success("Sản phẩm đã được xóa", HttpStatusCode.NoContent))
                : Results.Ok(ApiResponse.Fail(
                    HttpStatusCode.NotFound,
                    $"Không tìm thấy sản phẩm với id: `{id}`"));

        }

        #endregion

        #region Unit

        private static async Task<IResult> GetProductUnitById(
           int id,
           int unitId,
           IStoreRepository storeRepo,
           IMapper mapper)
        {
            var product = await storeRepo.GetProductByIdAsync(id);

            var unit = await storeRepo.GetUnitByIdAsync(product.Id, unitId);

            var unitDetail = mapper.Map<UnitDetailDto>(unit);

            return unitDetail != null
                    ? Results.Ok(ApiResponse.Success(unitDetail))
                    : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy loại sản phẩm với id: `{id}`"));
        }

        private static async Task<IResult> GetProductUnitByTag(
            int id,
           [FromRoute] string tag,
           IStoreRepository storeRepo,
           IMapper mapper)
        {
            var product = await storeRepo.GetProductByIdAsync(id);

            var unit = await storeRepo.GetUnitByTagAsync(product.Id, tag);

            var unitDetail = mapper.Map<UnitDetailDto>(unit);
            return unitDetail != null
                ? Results.Ok(ApiResponse.Success(unitDetail))
                : Results.Ok(ApiResponse.Fail(HttpStatusCode.NotFound, $"Không tìm thấy loại sản phẩm với tên loại: `{tag}`"));
        }



        #endregion

    }
}
