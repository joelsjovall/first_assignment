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
        Console.WriteLine(" ---------------------------");
        Console.WriteLine("|----------TRADING----------|");
        Console.WriteLine("|---------------------------|");
        Console.WriteLine("|Choose one of the following|");
        Console.WriteLine("|---------------------------|");
        Console.WriteLine("|-------1. Login------------|");
        Console.WriteLine("|-2. Register a new account-|");
        Console.WriteLine("|-------3. Exit-------------|");
        Console.WriteLine(" ---------------------------");
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
        Console.WriteLine("2. Browse others item");
        Console.WriteLine("3. Trademarket ");
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
                Console.WriteLine("List of other users tradeable items, if you wish to trade, enter the trademarket ");
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
                Console.WriteLine("----TRADEMARKET----");
                Console.WriteLine("1. Browse the market for something to trade ");
                Console.WriteLine("2. Incoming requests for your items ");
                Console.WriteLine("3. My sent requests ");
                Console.WriteLine("4. Your items available for trade");
                Console.WriteLine("5. Go back to main menu ");
                Console.WriteLine("6. All completed requests");

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
                        Console.ReadLine();
                        break;

                    case "2":       //Show pending requests, accept or deny traderequest
                        Console.Clear();
                        Console.WriteLine("All requests for your items: \n");

                        List<Trade> incomingTrades = new List<Trade>();         //list that will hold matching trades
                        foreach (Trade trade in trades)
                        {
                            if (trade.ReceiverEmail == active_user.Email)
                            {
                                incomingTrades.Add(trade);
                            }

                        }

                        if (incomingTrades.Count == 0)          //if there are no requests for logged in users items
                        {
                            Console.WriteLine("You have no traderequests");
                            Console.ReadLine();
                            break;
                        }

                        //prints a numbered list of requests
                        for (int i = 0; i < incomingTrades.Count; i++)
                        {
                            Trade current = incomingTrades[i];
                            Console.WriteLine((i + 1) + ". From: " + current.SenderEmail); //print row number and who sent request
                            Console.WriteLine("Item: " + current.ItemName + "(#" + current.ItemId + ")"); //print item name and id 
                            Console.WriteLine("Status: " + current.Status);         //print current status

                        }

                        //user picks a trade to respond to, or presses enter to go back
                        Console.WriteLine("Enter a number to respond");
                        string pickIncoming = Console.ReadLine();
                        if (string.IsNullOrWhiteSpace(pickIncoming))
                        {
                            break;
                        }

                        int choiceIncoming;
                        if (!int.TryParse(pickIncoming, out choiceIncoming) || choiceIncoming < 1 || choiceIncoming > incomingTrades.Count)
                        {
                            Console.WriteLine("Invalid choice");
                            Console.ReadLine();
                            break;
                        }

                        Trade selected = incomingTrades[choiceIncoming - 1];        //convert the 1 baased choice to a 0-based index
                        //ask reciever if they wish to accept or deny the request
                        Console.WriteLine("Accept (A) or deny (D) this request? ");
                        string decision = Console.ReadLine();
                        if (decision != null)
                        {
                            decision = decision.Trim().ToUpperInvariant();
                            if (decision == "A")
                            {
                                selected.Status = "Accepted";       //trade accepted
                                User receiverUser = active_user;        //logged in user is the reciever

                                User senderUser = null;         // find the person who requested the trade
                                for (int i = 0; i < users.Count; i++)
                                {
                                    if (users[i].Email == selected.SenderEmail)
                                    {
                                        senderUser = users[i];
                                        break;          //stop when we find sender 
                                    }
                                }
                                //find the index of item by ID in the recievers item list
                                int removeIndex = -1;
                                for (int i = 0; i < receiverUser.Items.Count; i++)
                                {
                                    if (receiverUser.Items[i].Id == selected.ItemId)
                                    {
                                        removeIndex = i;
                                        break;          //stop when item has been found
                                    }

                                }

                                //transfer ownership of item
                                Item itemToMove = receiverUser.Items[removeIndex];
                                receiverUser.Items.RemoveAt(removeIndex);
                                itemToMove.Email = senderUser.Email;
                                senderUser.Items.Add(itemToMove);

                                Console.WriteLine("Item transferred");




                            }
                            else if (decision == "D")
                            {
                                selected.Status = "Denied";
                            }
                            else
                            {
                                Console.WriteLine("You didnt Accept or Deny, therefore nothing happened");
                            }

                        }
                        Console.WriteLine("Status: " + selected.Status);
                        Console.ReadLine();
                        break;

                    case "3":       //View logged in users traderequests
                        Console.Clear();
                        Console.WriteLine("Trade request that you've sent");

                        //List of my sent request
                        List<Trade> mySent = new List<Trade>();
                        foreach (Trade trade in trades)         //scan all trades
                        {
                            if (trade.SenderEmail == active_user.Email)         //keep the ones i sent 
                            {
                                mySent.Add(trade);          //collect into mySent
                            }
                        }

                        //if none, tell user to return to main menu
                        if (mySent.Count == 0)
                        {
                            Console.WriteLine("You have haeven't sent any trade requests yet.");
                            Console.ReadLine();
                            break;
                        }
                        //print numbered list of trades user sent 
                        for (int i = 0; i < mySent.Count; i++)
                        {
                            Trade current = mySent[i];
                            Console.WriteLine((i + 1) + " To: " + current.ReceiverEmail);       //who i sent it to
                            Console.WriteLine(" Item: " + current.ItemName + "(#" + current.ItemId + ")");  //users itemname and id
                            Console.WriteLine(" Status: " + current.Status);    //status(pending,accepted,denied)
                            Console.WriteLine();
                        }

                        Console.WriteLine("Press enter to go back");
                        Console.ReadLine();
                        break;


                    case "4":       //see logged in users items available for trade
                        Console.Clear();
                        Console.WriteLine("Your items available for trade");

                        if (active_user.Items.Count == 0)       //if user has no items, inform and return
                        {
                            Console.WriteLine("You have no items, add some from the main menu");
                            Console.ReadLine();
                            break;
                        }

                        //list of users items with numbers 
                        for (int i = 0; i < active_user.Items.Count; i++)
                        {
                            Item item = active_user.Items[i];
                            Console.WriteLine((i + 1) + ". " + item.ShowItem());    //name, owner, description
                        }

                        Console.WriteLine("Press enter to go back");
                        Console.ReadLine();
                        break;

                    case "5":       //go back to main menu
                        break;

                    case "6":       // browse all completed requests 
                        Console.Clear();
                        Console.WriteLine("All users completed requests");

                        int shown = 0;      //how many trades 
                        for (int i = 0; i < trades.Count; i++)          //scan all trades
                        {
                            Trade currentTrade = trades[i];

                            if (currentTrade.Status != "Pending")       //show accepted/denied
                            {
                                shown++;            //increase the printed counter
                                Console.WriteLine(shown + ". From: " + currentTrade.SenderEmail);       //sender
                                Console.WriteLine("To : " + currentTrade.ReceiverEmail);        //reciever
                                Console.WriteLine("item: " + currentTrade.ItemName + " (#" + currentTrade.ItemId + ")");  //item details
                                Console.WriteLine("Status: " + currentTrade.Status);        //status ( denied, accepted )
                                Console.WriteLine();
                            }

                        }
                        //if nothing was printed, inform user
                        if (shown == 0)
                        {
                            Console.WriteLine("No completed requests... yet ");
                        }

                        Console.WriteLine("Press enter to go back");
                        Console.ReadLine();
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



