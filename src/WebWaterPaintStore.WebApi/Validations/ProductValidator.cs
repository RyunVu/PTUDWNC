﻿using FluentValidation;
using WebWaterPaintStore.Core.Entities;
using WebWaterPaintStore.WebApi.Models;

namespace WebWaterPaintStore.WebApi.Validations
{
    public class ProductValidator : AbstractValidator<ProductEditModel>
    {
        public ProductValidator()
        {
            RuleFor(post => post.Name)
            .NotEmpty().WithMessage("Tên sản phẩm không được bỏ trống")
            .MaximumLength(512).WithMessage("Chủ đề không được nhiều hơn 500 ký tự");

            RuleFor(s => s.ShortDescription)
                .NotEmpty()
                .WithMessage("Phần giới thiệu không được bỏ trống");


            RuleFor(s => s.Meta)
                .NotEmpty()
                .WithMessage("Metadata không được bỏ trống")
                .MaximumLength(1280)
                .WithMessage("Metadata không được nhiều hơn 1000 ký tự");

            RuleFor(s => s.UrlSlug)
                .NotEmpty()
                .WithMessage("Slug không được bỏ trống")
                .MaximumLength(1280)
                .WithMessage("Slug không được nhiều hơn 1000 ký tự");

            RuleFor(s => s.ImageUrl)
                .NotEmpty()
                .WithMessage("Ảnh không được bỏ trống");

            RuleFor(s => s.CategoryId)
                .NotEmpty()
                .WithMessage("Bạn phải chọn loại cho sản phẩm");

        }
    }
}
