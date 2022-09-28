namespace MyStore.Operations.CartItems;

using MyStore.Models;

public class BuildCartItemFromHash
{
    public Dictionary<string, object> Hash { get; set; }
    public CartItem CartItem { get; set; }

    public BuildCartItemFromHash(Dictionary<string, object> hash)
    {
        Hash = hash;
        CartItem = new CartItem();
    }

    public void run()
    {
        if(Hash.GetValueOrDefault("id") != null){
            CartItem.Id = Int32.Parse(Hash["id"].ToString());
        }

        if(Hash.GetValueOrDefault("itemId") != null){
            CartItem.ItemId = Int32.Parse(Hash["itemId"].ToString());
        }

        if(Hash.GetValueOrDefault("customerId") != null){
            CartItem.CustomerId = Int32.Parse(Hash["customerId"].ToString());
        }

        if(Hash.GetValueOrDefault("cost") != null){
            CartItem.Cost = Decimal.Parse(Hash["cost"].ToString());
        }

        if(Hash.GetValueOrDefault("quantity") != null){
            CartItem.Quantity = Int32.Parse(Hash["quantity"].ToString());
        }
    }
}