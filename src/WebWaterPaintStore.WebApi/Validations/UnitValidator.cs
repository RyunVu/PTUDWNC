using FluentValidation;
using WebWaterPaintStore.WebApi.Models;

namespace WebWaterPaintStore.WebApi.Validations
{
    public class UnitValidator : AbstractValidator<UnitEditModel>
    {
        public UnitValidator() {

            RuleFor(c => c.UnitTag)
                 .NotEmpty()
                 .WithMessage("Tên loại không được để trống")
                 .MaximumLength(128)
                 .WithMessage("Tên chủ đề tối đa 128 ký tự");

            RuleFor(c => c.Price)
                .NotEmpty()
                .WithMessage("Giá thành không được để trống");


            RuleFor(c => c.Quantity)
                .NotEmpty()
                .WithMessage("Số lượng tồn kho không được để trống");

            RuleFor(c => c.Discount);

            RuleFor(c => c.SoldCount);


        }
    }
}
