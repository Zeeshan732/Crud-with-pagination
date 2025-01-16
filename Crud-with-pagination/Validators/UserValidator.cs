using Crud_with_pagination.Models;
using FluentValidation;

namespace Crud_with_pagination.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator() {

            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");

        }
    }
}
