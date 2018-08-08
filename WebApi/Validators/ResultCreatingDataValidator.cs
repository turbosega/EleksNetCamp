using FluentValidation;
using Models.DataTransferObjects.Creating;

namespace WebApi.Validators
{
    public class ResultCreatingDataValidator : AbstractValidator<ResultCreatingDto>
    {
        public ResultCreatingDataValidator()
        {
            RuleFor(dto => dto.Score).Must(score => score > 0)
                                     .WithMessage("Score can not be less than 1");
        }
    }
}