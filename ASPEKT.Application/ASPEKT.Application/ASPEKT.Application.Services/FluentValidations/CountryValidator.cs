using ASPEKT.Application.DTOS.Country;
using FluentValidation;

namespace ASPEKT.Application.Services.FluentValidations
{
    public class CountryValidator : AbstractValidator<CountryDto>
    {
        public CountryValidator()
        {
            RuleFor(x => x.CountryName).NotEmpty().WithMessage("The country name must be entered!!")
            .MaximumLength(50).WithMessage("The country name must be less than a 50 characters");
        }
    }
}
