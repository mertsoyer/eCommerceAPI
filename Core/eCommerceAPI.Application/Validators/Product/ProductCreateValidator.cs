using eCommerceAPI.Application.ViewModels.Product;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerceAPI.Application.Validators.Product
{
    public class ProductCreateValidator : AbstractValidator<ProductCreateViewModel>
    {
        public ProductCreateValidator()
        {
            RuleFor(x => x.Name).
                NotEmpty().
                NotNull().
                WithMessage("Ürün adı boş geçilemez.")
                .MaximumLength(150)
                .MinimumLength(5)
                .WithMessage("Karakter sayısı 5 ile 150 arasında olmalıdır.");

            RuleFor(x => x.Stock)
                .NotNull()
                .NotEmpty()
                .WithMessage("Stok bilgisi boş geçilemez")
                .Must(x => x > 0)
                .WithMessage("Stok bilgisi negatif değer olamaz.");

            RuleFor(x => x.Price)
                .NotNull()
                .NotEmpty()
                .WithMessage("Fiyat bilgisi boş geçilemez")
                .Must(x => x > 0)
                .WithMessage("Fiyat bilgisi negatif değer olamaz.");
        }
    }
}
