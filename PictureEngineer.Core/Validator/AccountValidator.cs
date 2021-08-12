using FluentValidation;
using PictureEngineer.Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PictureEngineer.Core.Validator
{
    public class AccountValidator : AbstractValidator<AccountLoginDto>
    {
        public AccountValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Bạn cần nhập email")
                .NotNull()
                .EmailAddress().WithMessage("Email không hợp lệ.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Bạn cần nhập mật khẩu")
                .NotNull();
        }
    }


    public class AccountForgotPasswordValidator : AbstractValidator<ChangeForgotPasswordDto>
    {
        public AccountForgotPasswordValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Code)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Password).Password();

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Nhập lại đúng với mật khẩu.");
        }
    }

    public class AccountRegisterValidator : AbstractValidator<RegistryAccount>
    {
        public AccountRegisterValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.FullName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Password).Password();

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Nhập lại đúng với mật khẩu.");
        }
    }

    public class ProfileValidator : AbstractValidator<ProfileDto>
    {
        public ProfileValidator()
        {
            RuleFor(x => x.FullName)
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty()
                .NotNull();

            RuleFor(x => x.PhoneNumber)
                .NotEmpty()
                .NotNull();
        }
    }

    public class ProfilePasswordValidator : AbstractValidator<ProfilePassword>
    {
        public ProfilePasswordValidator()
        {
            RuleFor(x => x.PasswordOld).Password();
            RuleFor(x => x.PasswordNew).Password();
            RuleFor(x => x.ConfirmPasswordNew).Equal(x => x.PasswordNew);
        }
    }
}
