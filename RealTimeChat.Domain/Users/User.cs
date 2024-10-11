using FluentValidation;
using RealTimeChat.Domain.Repositories;
using RealTimeChat.Domain.Shared.Aggregates;
using RealTimeChat.Domain.Shared.Validation;

namespace RealTimeChat.Domain.Users;

public class User : BaseDomain, IValidationModel<User>
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string UserName { get; private set; }
    public string PasswordHash { get; set; }
    public string PasswordSalt { get; set; }
    public bool IsOnline { get; private set; } = false;
    public DateTime? LastSeen { get; private set; }

    public AbstractValidator<User> Validator => new UserValidator();

    private User()
    {
        Email = string.Empty;
        UserName = string.Empty;
        PasswordHash = string.Empty;
        PasswordSalt = string.Empty;
    }

    public static User Create(int? id, string email, string username, string passwordHash, string passwordSalt)
        => new()
        {
            Id = id ?? 0,
            Email = email,
            UserName = username,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt
        };

    public async Task<int> Create(IUserRepository repository, IValidationEngine validation)
    {
        validation.Validate(this);
        return await repository.Create(this);
    }
}
