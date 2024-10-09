namespace RealTimeChat.Domain.Users;

public class User
{
    private User()
    {
        UserName = string.Empty;
        Email = string.Empty;
    }

    public int Id { get; private set; }
    public string UserName { get; private set; }
    public string Email { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public bool IsOnline { get; private set; }
    public DateTime LastSeen { get; private set; }

}
