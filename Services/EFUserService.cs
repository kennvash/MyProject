namespace MyStore.Services;

using MyStore.Models;
using MyStore.Data;
using MyStore.Operations.Users;

public class EFUserService : IUserService
{
    private readonly DataContext _dataContext;

    public EFUserService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public User FindByToken(string token)
    {
        return _dataContext.Users.SingleOrDefault(u => u.Token.Equals(token));
    }

    public User FindByUsername(string username)
    {
        return _dataContext.Users.SingleOrDefault(u => u.Username.Equals(username));
    }

    public User Register(string username, string password, string role)
    {
        HashPassword hasher = new HashPassword(password);
        hasher.run();

        string encryptedPassword = hasher.Hash;

        User user = new User() {
            Username = username,
            EncryptedPassword = encryptedPassword,
            Role = role
        };

        _dataContext.Users.Add(user);
        _dataContext.SaveChanges();

        return user;
    }
}