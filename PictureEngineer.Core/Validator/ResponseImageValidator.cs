using FluentValidation;
using PictureEngineer.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Validator
{
    public class ResponseImageValidator : AbstractValidator<ResponseImageDto>
    {
        public ResponseImageValidator()
        {
            RuleFor(x => x.FilePath).NotEmpty().NotNull();
            RuleFor(x => x.Language).NotEmpty().NotNull();
        }
    }
}
