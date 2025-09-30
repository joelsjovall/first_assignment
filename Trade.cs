// Reciever
// Sender
// Status
// Items
// recieveremail, itemid, itemname, tradestatus status

namespace App;

public class Trade
{
    public string SenderEmail;
    public string RecieverEmail;
    public int ItemId;
    public string ItemName;
    public string TradeStatus;

    public Trade(string senderEmail, string recieverEmail, int itemId, string itemName, string tradeStatus)
    {
        SenderEmail = senderEmail;
        RecieverEmail = recieverEmail;
        ItemId = itemId;
        ItemName = itemName;
        TradeStatus = tradeStatus;
    }
}

public enum TradeStatus
{
    Pending,
    Denied,
    Accepted,
}
