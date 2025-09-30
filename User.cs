// email
// password


namespace App;

public class User
{
    public readonly string Email;           //user logs in through their email
    public string Password;         //users password

    public readonly List<Item> Items = new List<Item>();
    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
}


