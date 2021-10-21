using FluentValidation;
using ProductService.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductService.Data.FluentValidators
{
    public class DtoValidators
    {
        public class CategoryDtoValidator : AbstractValidator<CategoryDto>
        {
            public CategoryDtoValidator()
            {
                this.RuleFor(t => t.CategoryId);
                this.RuleFor(t => t.Name).NotEmpty().WithMessage("Its a required field").MinimumLength(5).WithMessage("Name should be more than 5 characters");
               // this.RuleFor(t => t.Value).NotEmpty().WithMessage("Its a required field").MinimumLength(3).WithMessage("Name should be more than 5 characters");
                this.RuleForEach(t => t.Products)
                    .NotEmpty()
                    .WithMessage("Values in the product cannot be empty");
            }
        }
        public class ProductDtoValidator : AbstractValidator<ProductDto>
        {
            public ProductDtoValidator()
            {
                this.RuleFor(t => t.ProductId);
                this.RuleFor(t => t.Name).NotEmpty().WithMessage("Name is Required").MinimumLength(5).WithMessage("Name should be more than 5 characters");
                this.RuleFor(t => t.Description).NotEmpty().WithMessage("Description is Required").MinimumLength(5).WithMessage("Description should be more than 5 characters");
                this.RuleFor(t => t.Brand).NotEmpty().WithMessage("Brand is Required").MinimumLength(3).WithMessage("Brand should be more than 3 characters");
                this.RuleFor(t => t.Price).NotEmpty().WithMessage("Price field is required");
            }
        }
    }
}