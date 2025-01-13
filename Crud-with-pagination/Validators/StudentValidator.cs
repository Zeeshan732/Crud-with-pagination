using FluentValidation;

using Crud_with_pagination.Models;

namespace Crud_with_pagination.Validators;

public class StudentValidator : AbstractValidator<TblStudent>
{
    public StudentValidator()
    {
        RuleFor(s => s.Name).NotEmpty().MaximumLength(50);
        RuleFor(s => s.Email).NotEmpty().MaximumLength(50).EmailAddress().WithMessage("Emial is not Valid");
        RuleFor(s => s.Phone).NotEmpty().MaximumLength(15).WithMessage("Phone number not valid");
        RuleFor(s => s.Department).NotEmpty().MaximumLength(10).WithMessage("Department is Required");
       
    }
}
