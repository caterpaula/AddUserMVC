using FluentValidation;

namespace UNiDAYSHomework.Models
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.EmailAddress)
                .NotEmpty().WithMessage("Email address must be supplied.")
                .EmailAddress().WithMessage("Invalid email address.");

            RuleFor(user => user.Password)
                .NotEmpty().WithMessage("Password is required.")
                .Length(6, 32).WithMessage("Password must be between 6 and 32 characters long.");
        }
    }
}