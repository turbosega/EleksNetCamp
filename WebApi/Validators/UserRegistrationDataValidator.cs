using FluentValidation;
using Models.DataTransferObjects.Creating;

namespace WebApi.Validators
{
    public class UserRegistrationDataValidator : AbstractValidator<UserRegistrationDto>
    {
        public UserRegistrationDataValidator()
        {
            RuleFor(dto => dto.Login).Must(login => !login.Contains("  "))
                                     .WithMessage("Login can not contain two and more spaces in a row");

            RuleFor(dto => dto.Password.ToLower()).NotEqual(dto => dto.Login.ToLower())
                                                  .WithMessage("Password must not match with login");

            RuleFor(dto => dto.Avatar).NotNull()
                                      .WithMessage("Avatar must be provided");
        }
    }
}