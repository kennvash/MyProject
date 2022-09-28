namespace MyStore.Operations.CartItems;

using MyStore.Models;

public class ValidateSaveCartItem : Validator
{
    public Int32 Id { get; set; }
    public Int32 ItemId { get; set; }
    public Int32 Quantity { get; set; }
    public Decimal Cost { get; set; }
    public Int32 CustomerId { get; set;}
    public Item Item { get; set; }
    public Customer Customer { get; set; }

    public void InitializeParameters(Dictionary<string, object> hash)
    {
        if(hash.GetValueOrDefault("id") != null){
            this.Id = Int32.Parse(hash["id"].ToString());
        }

        if(hash.GetValueOrDefault("itemId") != null){
            this.ItemId = Int32.Parse(hash["itemId"].ToString());
        }

        if(hash.GetValueOrDefault("customerId") != null){
            this.CustomerId = Int32.Parse(hash["customerId"].ToString());
        }

        if(hash.GetValueOrDefault("quantity") != null){
            this.Quantity = Int32.Parse(hash["quantity"].ToString());
        }

        if(hash.GetValueOrDefault("cost") != null){
            this.Cost = Decimal.Parse(hash["cost"].ToString());
        }
    }

    public override void run()
    {
        if(this.ItemId == null || this.ItemId.Equals("")){
            String msg = "Item id is required!";
            this.AddError(msg, "itemId");
        }

        if(this.CustomerId == null || this.CustomerId.Equals("")){
            String msg = "Customer id is required!";
            this.AddError(msg, "customerId");
        }

        if(this.Quantity == null || this.Quantity.Equals("")){
            String msg = "Quantity is required!";
            this.AddError(msg, "quantity");
        }

        if(this.Cost == null || this.Cost.Equals("")){
            String msg = "Cost is required!";
            this.AddError(msg, "cost");
        }
    }
}