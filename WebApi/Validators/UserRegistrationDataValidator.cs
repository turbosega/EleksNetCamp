using FluentValidation;
using Models.DataTransferObjects;

namespace WebApi.Validators
{
    public class UserRegistrationDataValidator : AbstractValidator<UserDto>
    {
        public UserRegistrationDataValidator()
        {
            RuleFor(dto => dto.Login).Must(login => !login.Contains("  "))
                                     .WithMessage("Login can not contain two and more spaces in a row");
            RuleFor(dto => dto.Password.ToLower()).NotEqual(dto => dto.Login.ToLower())
                                                  .WithMessage("Password must not match with login");
        }
    }
}