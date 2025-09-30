// Reciever
// Sender
// Status
// Items
// recieveremail, itemid, itemname, tradestatus status a 

namespace App;

public class Trade
{
    public string SenderEmail;          //who initiates the trade
    public string ReceiverEmail;        //who recieves the traderequest
    public int ItemId;      //every tradeable item should have an id
    public string ItemName;     //every tradeable item needs to have a name 
    public string Status;       // status of 

    public Trade(string senderEmail, string receiverEmail, int itemId, string itemName, string status)
    {
        SenderEmail = senderEmail;
        ReceiverEmail = receiverEmail;
        ItemId = itemId;
        ItemName = itemName;
        Status = status;
    }
}

public enum TradeStatus
{
    Pending,
    Denied,
    Accepted,
}
