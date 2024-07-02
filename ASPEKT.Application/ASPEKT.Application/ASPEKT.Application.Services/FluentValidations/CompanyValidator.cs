using ASPEKT.Application.DTOS.Company;
using FluentValidation;

namespace ASPEKT.Application.Services.FluentValidations
{
    public class CompanyValidator : AbstractValidator<CompanyDto>
    {
        public CompanyValidator()
        {
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("The company name must be entered!!")
            .MaximumLength(50).WithMessage("The company name must be less than a 50 characters");
        }
        

    }
}
