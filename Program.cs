using System.Runtime.CompilerServices;
using App;

List<User> users = new List<User>();
List<Item> items = new List<Item>();

User? active_user = null;

bool running = true;

while (running)
{
    Console.Clear();

    if (active_user == null)
    {
        Console.WriteLine("TRADING");
        Console.WriteLine("--------");
        Console.WriteLine("Choose one of the following");
        Console.WriteLine("--------");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register a new account");
        string userInput = Console.ReadLine();

        switch (userInput)
        {
            case "1":
                Console.WriteLine("Username: ");
                string username = Console.ReadLine();
                Console.WriteLine("Password: ");
                string password = Console.ReadLine();
                break;

            case "2":
                Console.WriteLine("Choose your new username");
                string newUsername = Console.ReadLine();
                Console.WriteLine("Choose your new password");
                string newPassword = Console.ReadLine();
                break;
        }

    }

}

