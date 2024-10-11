using FluentValidation;

namespace RealTimeChat.Domain.Users
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().MaximumLength(256);
            RuleFor(x => x.Email).NotEmpty().EmailAddress().MaximumLength(256);
            RuleFor(x => x.PasswordHash).NotEmpty().MaximumLength(256);
            RuleFor(x => x.PasswordSalt).NotEmpty().MaximumLength(256);
        }

    }
}
