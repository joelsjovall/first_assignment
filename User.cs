// email
// password


namespace App;

public class User
{
    public string Email;           //user logs in through their email
    public string Password;              //users password

    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
}


