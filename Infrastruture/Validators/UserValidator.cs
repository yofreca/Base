using Core.DTOs;
using FluentValidation;

namespace Infrastruture.Validators
{
    public class UserValidator : AbstractValidator<UserDto>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName)
                .NotNull()
                .NotEmpty()
                .Length(3, 100);

            RuleFor(user => user.LastName)
                .NotNull()
                .NotEmpty()
                .Length(3, 100);

            RuleFor(user => user.Email)
                .NotEmpty()
                .EmailAddress();
        }
    }
}
