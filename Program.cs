using System.Net;
using System.Runtime.CompilerServices;
using App;





List<User> users = new List<User>();
List<Item> items = new List<Item>();
List<Trade> trades = new List<Trade>();
int nextTradeId = 1;
int nextItemId = 1;

User? active_user = null;

bool running = true;

while (running)
{
    try { Console.Clear(); } catch { }

    if (active_user == null)
    {
        Console.WriteLine("TRADING"); //menyval för användaren: 
        Console.WriteLine("--------");
        Console.WriteLine("Choose one of the following");
        Console.WriteLine("--------");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register a new account");
        string userInput = Console.ReadLine(); // läser användarens menyval

        switch (userInput)
        {
            case "1": // Login
                try { Console.Clear(); } catch { }

                Console.WriteLine("Email: ");
                string emailLogin = Console.ReadLine();

                try { Console.Clear(); } catch { }

                Console.WriteLine("Password: ");
                string passwordLogin = Console.ReadLine();

                User found = null;
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Email == emailLogin && users[i].Password == passwordLogin)
                    {
                        found = users[i];
                        break;
                    }

                }

                if (found == null)
                {
                    Console.WriteLine("Wrong email or password, try again");
                    Console.ReadLine();
                }
                else
                {
                    active_user = found;
                }
                break;


            case "2": //Register new account
                Console.Clear();
                Console.WriteLine("Choose your new email: ");
                string newEmail = Console.ReadLine();

                Console.WriteLine("Choose your new password: ");
                string newPassword = Console.ReadLine();

                users.Add(new User(newEmail, newPassword));

                Console.WriteLine("Account was created.");
                Console.ReadLine();
                break;
        }

    }
    else
    {
        Console.WriteLine("Logged in as : " + active_user.Email);
        Console.WriteLine("Choose an option below: ");
        Console.WriteLine("1. Upload an item for trade ");
        Console.WriteLine("2. Browse others item for trade"); // if jag trycker på denna så ska jag kunna requesta deras items
        Console.WriteLine("3. All offers for your items ");
        Console.WriteLine("4. Logout current user ");

        string userInput = Console.ReadLine();

        switch (userInput)
        {
            case "1": // Upload item for trade
                Console.Clear();
                Console.WriteLine("Item name: ");
                string name = Console.ReadLine() ?? "";

                Console.WriteLine("Item description: ");
                string description = Console.ReadLine() ?? "";

                Item newItem = new Item(name, description, active_user.Email, nextItemId++);

                active_user.Items.Add(newItem);

                Console.WriteLine("Item added");
                Console.ReadLine();
                break;

            case "2": // Browse other peoples trades
                Console.Clear();
                Console.WriteLine("Other users tradeable items: ");
                foreach (User user in users)
                {
                    foreach (Item item in user.Items)
                    {
                        if (user.Items != active_user.Items)
                        {
                            Console.WriteLine(item.ShowItem());
                        }
                    }
                }
                Console.ReadLine();
                break;

            case "3":
                Console.Clear();
                Console.WriteLine("");
                break;

            case "4": // Logout
                active_user = null;
                break;

        }

    }

}



// void p(string input)
// {
//     Console.WriteLine(input);
// }



