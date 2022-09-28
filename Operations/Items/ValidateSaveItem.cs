namespace MyStore.Operations.Items;

public class ValidateSaveItem : Validator
{
    public Int32 Id { get; set; }
    public String Name { get; set; }
    public Decimal Cost { get; set; }

    public void InitializeParameters(Dictionary<string, object> hash)
    {
        if(hash.GetValueOrDefault("id") != null){
            this.Id = Int32.Parse(hash["id"].ToString());
        }

        if(hash.GetValueOrDefault("name") != null){
            this.Name = hash["name"].ToString();
        }

        if(hash.GetValueOrDefault("cost") != null){
            this.Cost = Decimal.Parse(hash["cost"].ToString());
        }
    }


    public override void run()
    {
        if(this.Name == null || this.Name.Equals("")){
            String msg = "Item name is required!";
            this.AddError(msg, "itemName");
        }

        if(this.Cost == null || this.Cost.Equals("")){
            String msg = "Item cost is required!";
            this.AddError(msg, "itemCost");
        }
    }
}