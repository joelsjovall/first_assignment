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
        Console.WriteLine("3. Exit");
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

            case "3":       //simple code that exits the program
                running = false;
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


        switch (userInput)          //choose which action to run based on the users input
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
                        if (user.Items != active_user.Items)        //show items, but not the current logged in users items.
                        {
                            Console.WriteLine(item.ShowItem());
                        }
                    }
                }
                Console.ReadLine();
                break;

            case "3":       //The trade menu with options
                Console.Clear();
                Console.WriteLine("----TRADE----");
                Console.WriteLine("1. Browse the market for something to trade ");
                Console.WriteLine("2. Incoming requests for your items ");
                Console.WriteLine("3. My sent requests ");
                Console.WriteLine("4. Your items available for trade");
                Console.WriteLine("5. Go back to main menu ");

                string tradeInput = Console.ReadLine();


                switch (tradeInput)      //choose which action to run based on the users input
                {
                    case "1":       //Browse the market
                        Console.Clear();
                        Console.WriteLine("Other users tradeable items: \n");       // /n to make content drop down to next line

                        List<Item> market = new List<Item>();       //all items from other users

                        // int printed = 0;

                        foreach (User user in users)
                        {
                            if (user == active_user)
                                continue;

                            foreach (Item item in user.Items)       //for every 
                            {
                                market.Add(item);       //add item
                                Console.WriteLine(market.Count + ". " + item.ShowItem());            //
                            }

                        }

                        if (market.Count == 0)          //if theres 0 items available
                        {
                            Console.WriteLine("No items available for trade");
                            Console.ReadLine();
                            break;
                        }

                        Console.WriteLine("Request an item by entering a number, or go back by pressing enter ");
                        string pick = Console.ReadLine();       //pick the typed number
                        if (string.IsNullOrWhiteSpace(pick))        //go back if user pressed enter
                        {
                            break;
                        }

                        int choice;         //holds users choice (number)
                        //
                        if (!int.TryParse(pick, out choice) || choice < 1 || choice > market.Count)
                        {
                            Console.WriteLine("Invalid choice. ");          //tell that to the user
                            Console.ReadLine();
                            break;
                        }
                        Item chosen = market[choice - 1];

                        //create and store a new trade request: 
                        trades.Add(new Trade(active_user.Email, chosen.Email, chosen.Id, chosen.Name, "Pending"));
                        Console.WriteLine("Trade request sent to " + chosen.Email + " for " + chosen.Name + ". Let's hope they accept!");           //confirmationtext to the user 
                        Console.Read();
                        break;

                    case "2":       //Show pending requests, accept or deny traderequest
                        Console.Clear();
                        Console.WriteLine("All requests for your items: \n");

                        List<Trade> incomingTrades = new List<Trade>();         //list that will hold matching trades
                        foreach (Trade t in trades)
                        {



                            break;

                    case "3":       //View logged in users traderequests
                                break;
                            case "4":       //see your items
                                break;
                            case "5":       //go back 
                                break;
                            }
                            break;

            case "4": // Logout
                                active_user = null;
                                break;

                            }

                        }

                }



// 



