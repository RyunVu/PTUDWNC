﻿using Mapster;
using WebWaterPaintStore.Core.DTO;
using WebWaterPaintStore.Core.Entities;
using WebWaterPaintStore.WebApi.Models;

namespace WebWaterPaintStore.WebApi.Mapsters
{
    public class MapsterConfiguration : IRegister{
    
        public void Register(TypeAdapterConfig config) {
           
            config.NewConfig<Category, CategoryDto>();
            config.NewConfig<Category, CategoryItem>()
                .Map(dest => dest.ProductsCount,
                    src => src.Products == null ? 0 : src.Products.Count);

            config.NewConfig<Product, ProductDto>();
            config.NewConfig<Product, ProductDetail>();
                //.Map(dest => dest.UnitCount,
                //    src => src.UnitDetails == null ? 0 : src.UnitDetails.Count);

            config.NewConfig<UnitDetail, UnitDetailDto>();
            config.NewConfig<UnitDetail, UnitDetailItem>();
            config.NewConfig<UnitDetail, UnitDetailInfo>();

            config.NewConfig<ProductEditModel, Product>()
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.UnitDetails);

        }
    }
}
