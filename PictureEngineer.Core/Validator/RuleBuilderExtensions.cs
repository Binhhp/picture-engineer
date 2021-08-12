using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Validator
{
    public static class RuleBuilderExtensions
    {
        public static IRuleBuilder<T, string> Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            var options = ruleBuilder
                          .NotEmpty()
                          .WithMessage("Bạn cần nhập mật khẩu.")
                          .NotNull()
                          .WithMessage("Bạn cần nhập mật khẩu.")
                          .MinimumLength(4)
                          .MaximumLength(16)
                          .WithMessage(string.Format(@"Mật khẩu phải dài ít nhất {1} và tối đa {0} ký tự.", 16, 8));

            return options;
        }
    }
}
