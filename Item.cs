// name
// description

namespace App;

public class Item
{
    public string Name;   //name of the trading item
    public string Description; // description of the item
    public string Email; //who owns each item
    public readonly int Id;  // each item should have an id, readonly ensures that theres no way of accidentaly changing the id 



    // 
    public Item(string name, string description, string email, int id)
    {
        Name = name;
        Description = description;
        Email = email;
        Id = id;
    }
}

