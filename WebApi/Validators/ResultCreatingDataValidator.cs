using FluentValidation;
using Models.DataTransferObjects;

namespace WebApi.Validators
{
    public class ResultCreatingDataValidator : AbstractValidator<ResultDto>
    {
        public ResultCreatingDataValidator()
        {
            RuleFor(dto => dto.Score).Must(score => score > 0);
        }
    }
}