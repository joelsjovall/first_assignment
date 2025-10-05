This trading system is written in c# and allows users to trade items with other users, in order to run this program you need git installed and configured.
How to run:

Dotnet run in the project folder in git bash terminal

How to use:

Main menu

1. Login using email + password
2. Register a new account
3. Exit/Quit

After you've logged in you're able to:

1. Upload an item for trade, this is where you enter name and description of the item, and the item gets a unique ID and the users email as owner of the item.
2. Browse others items, this is a list of other users items.
3. The trademarket, where all the action is. After pressing number 3 to enter the trademarket, you have several options again:
   1. Browse the market for something to trade, a numbered list of other users items where you can enter a number to send a pending trade request to the user who owns that item.
   2. Incoming requests for your items, here you can view requests sent by other users for your(logged in users) items. You can then pick a number off that list, and press A to accept, or D to deny.
   3. This is where you can see all requests that you've sent other users for their items + their current status.
   4. List of all your own items that are available to other users.
   5. Go back to the main menu
   6. All completed requests, this is where you can show ALL accepted or denied requests for all users. I opted for being able to see all finished requests or just the ones of the logged in user, but I decided to go this route.
4. And then you got Logout where you exit the program.

Ive used several .cs files in my project to keep my project structured and make the code more readable. Each .cs file is named after what they contain

- Program.cs is the main loop, all main interactions happens here
- User.cs is the user model, contains Email, Password and the users lists
- Item.cs is the item model, contains Name, Description, Email owner ID and ShowItem() for printing
- Trade.cs contains the trade model, SenderEmail, ReceiverEmail, Itemid, ItemName, Status (pending, Accepted, Denied)

There are some future improvements that I would've implemented if I had the time. I'd like to add timestamps on trades for example, unique email check on registration and basic UX for user experience. These are just 3 of many changes or addition that i would've added if I could. I prioritized getting the code to work correctly which is one reason as to why I haven't been able to implement automatic save/load yet. I chose not to use Debug.Assert becaise I didn't quite know where to put it, and I wasn't sure how it worked. I used "has-a" composition because a user has a list of items, and since a user isnt a kind of item we need an ownership (one user has many items). Inheritance wouldn't work here since it uses a "is-a" relationship, and that just wouldn't work.

To summarize what the program is able to do:
-Create an account and log in and out of accounts
-Add items with unique id's
-Browse logged in users and other users items
-Send trade request for other users items
-See incoming requests and accept or deny them
-When accepted, the item is removed from the receiver and assigned to the sender
-See sent requests and their status
-See all completed requests for all users
