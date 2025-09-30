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
    public Item(string name, string description, string email, int id) //constructor that takes name, description, email and id 
    {
        Name = name;
        Description = description;
        Email = email;
        Id = id;
    }

    public string ShowItem()
    {
        if (string.IsNullOrWhiteSpace(Description))         //check if description is either null, empty or whitespace
            return $"{Name} (#{Id}) - owner: {Email}";      //return without description

        return $"{Name} (#{Id}) - owner: {Email} - {Description}";          //returns with description
    }
}

