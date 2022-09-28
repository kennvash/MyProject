namespace MyStore.Operations.Users;

using MyStore.Models;

public class GenerateToken
{
    public string Token { get; private set; }

    private User _user;

    public GenerateToken(User user)
    {
        _user = user;
    }

    public void run()
    {
        Token = (Guid.NewGuid()).ToString();
    }
}