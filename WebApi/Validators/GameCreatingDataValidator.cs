using FluentValidation;
using Models.DataTransferObjects.Creating;

namespace WebApi.Validators
{
    public class GameCreatingDataValidator : AbstractValidator<GameCreatingDto>
    {
        public GameCreatingDataValidator() {
            RuleFor(dto => dto.Title).Must(title => !title.Contains("  "))
                                     .WithMessage("Title can not contain two and more spaces in a row");
        }
    }
}