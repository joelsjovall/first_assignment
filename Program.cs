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
        //Menu for the user 
        Console.WriteLine("TRADING");
        Console.WriteLine("--------");
        Console.WriteLine("Choose one of the following");
        Console.WriteLine("--------");
        Console.WriteLine("1. Login");
        Console.WriteLine("2. Register a new account");
        string userInput = Console.ReadLine();          //reads the users input

        switch (userInput)
        {
            case "1": // Login
                try { Console.Clear(); } catch { }

                Console.WriteLine("Email: ");
                string emailLogin = Console.ReadLine();         //reads users email

                try { Console.Clear(); } catch { }

                Console.WriteLine("Password: ");
                string passwordLogin = Console.ReadLine();      //reads users password

                //search in the users list for matching email and password
                User found = null;
                for (int i = 0; i < users.Count; i++)       //check each user 
                {
                    if (users[i].Email == emailLogin && users[i].Password == passwordLogin)
                    {
                        found = users[i];
                        break;      //stop searching after finding match
                    }

                }

                if (found == null) // if the email or password is wrong, user won't be logged in
                {
                    Console.WriteLine("Wrong email or password, try again");
                    Console.ReadLine();
                }
                else //if the email and password matches the users login credentials, log in
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
    {   //menu for logged in user
        Console.WriteLine("Logged in as : " + active_user.Email);       //shows which user is logged in
        Console.WriteLine("Choose an option below: ");
        Console.WriteLine("1. Upload an item for trade ");
        Console.WriteLine("2. Browse others item for trade"); // if jag trycker på denna så ska jag kunna requesta deras items
        Console.WriteLine("3. Trade ");
        Console.WriteLine("4. Logout current user ");

        string userInput = Console.ReadLine();          //reads the logged in users input


        switch (userInput)
        {
            case "1": // Upload item for trade
                Console.Clear();
                Console.WriteLine("Item name: ");
                string name = Console.ReadLine();

                Console.WriteLine("Item description: ");
                string description = Console.ReadLine();

                Item newItem = new Item(name, description, active_user.Email, nextItemId++);        //creates item with a new id

                active_user.Items.Add(newItem);         //stores this new item in the logged in users item list 

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



