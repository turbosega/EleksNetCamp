using FluentValidation;
using Models.DataTransferObjects;

namespace WebApi.Validators
{
    public class GameCreatingDataValidator : AbstractValidator<GameDto>
    {
        public GameCreatingDataValidator() {
            RuleFor(dto => dto.Title).Must(title => !title.Contains("  "))
                                     .WithMessage("Title can not contain two and more spaces in a row");
        }
    }
}