// email
// password


namespace App;

public class User
{
    public string Email; //användaren loggar in genom sin email
    string Password; //användarens lösenord

    public User(string email, string password)
    {
        Email = email;
        Password = password;
    }
}


