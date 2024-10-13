using FluentValidation;
using RealTimeChat.Domain.Repositories;
using RealTimeChat.Domain.Shared.Aggregates;
using RealTimeChat.Domain.Shared.Exceptions;
using RealTimeChat.Domain.Shared.Validation;

namespace RealTimeChat.Domain.Users;

public class User : BaseDomain, IValidationModel<User>
{
    public int Id { get; private set; }
    public string Email { get; private set; }
    public string UserName { get; private set; }
    public string PasswordHash { get; private set; }
    public string PasswordSalt { get; private set; }
    public bool IsOnline { get; private set; } = false;
    public DateTime? LastSeen { get; private set; }
    public bool IsVerified { get; private set; } = false;
    public string? EmailOTP { get; private set; }
    public string? ProcessId { get; private set; }

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

    public async Task<int> Create(IUsersRepository repository, IValidationEngine validation)
    {
        validation.Validate(this);
        return await repository.Create(this);
    }
    public async Task<bool> Update(IUsersRepository repository, IValidationEngine validation)
    {
        validation.Validate(this);
        return await repository.Update(this);
    }

    public static async Task<User> GetByUsername(string username, IUsersRepository repository)
    {
        var user = await repository.GetByUsername(username);
        if (user == null)
        {
            throw new DataNotFoundException("User not found.");
        }
        return user;
    }
    public static async Task<User> GetByProcessId(string processId, IUsersRepository repository)
    {
        var user = await repository.GetByProcessId(processId);
        if (user == null)
        {
            throw new DataNotFoundException("User not found.");
        }
        return user;
    }

    public void SetIsOnline(bool isOnline) => IsOnline = isOnline;
    public void SetIsVerified(bool isVerified) => IsVerified = isVerified;
    public void SetEmailOTP(string emailOTP) => EmailOTP = emailOTP;
    public void SetProcessId(string processId) => ProcessId = processId;


}
