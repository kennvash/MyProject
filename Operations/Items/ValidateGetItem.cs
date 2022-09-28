namespace MyStore.Operations.Items;

using MyStore.Models;

public class ValidateGetItem : Validator
{
    public Item Item { get; set; }

    public ValidateGetItem(Item item)
    {
        Item = item;
    }

    public override void run()
    {
        if(this.Item == null){
            String msg = "Item not found!";
            this.AddError(msg, "item");
        }
    }
}