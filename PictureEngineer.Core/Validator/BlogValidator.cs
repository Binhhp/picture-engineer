using FluentValidation;
using PictureEngineer.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Validator
{
    public class BlogValidator : AbstractValidator<BlogDto>
    {
        public BlogValidator()
        {
            RuleFor(x => x.MetaTitle)
                .NotEmpty()
                .WithMessage("Bạn cần nhập meta.")
                .NotNull();

            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Bạn cần nhập tiêu đề.")
                .NotNull();

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Bạn cần nhập mô tả.")
                .NotNull();

            RuleFor(x => x.Contents)
                .NotEmpty()
                .WithMessage("Bạn cần nhập nôi dung.")
                .NotNull();

            RuleFor(x => x.FileUpload).SetValidator(new FileValidator());
        }
    }
}
