// name
// description

namespace App;

public class Item                   
{
    public string Name;             //namnet på tradingitem
    public int Quantity;            //hur många det finns av varje tradingitem

    public Item(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }
}

// 