using ASPEKT.Application.DTOS.Contact;
using FluentValidation;

namespace ASPEKT.Application.Services.FluentValidations
{
    public class ContactValidator : AbstractValidator<ContactDto>
    {
        public ContactValidator()
        {
            RuleFor(x => x.ContactName).NotEmpty().WithMessage("The contact name must be entered!!")
            .MaximumLength(50).WithMessage("The contact name must be less than a 50 characters");
        }
    }
}
