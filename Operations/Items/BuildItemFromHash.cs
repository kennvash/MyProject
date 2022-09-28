namespace MyStore.Operations.Items;

using MyStore.Models;

public class BuildItemFromHash
{
    public Dictionary<string, object> Hash { get; set; }
    public Item Item { get; set; }

    public BuildItemFromHash(Dictionary<string, object> hash)
    {
        Hash = hash;
        Item = new Item();
    }

    public void run()
    {
        if(Hash.GetValueOrDefault("name") != null){
            Item.Name = Hash["name"].ToString();
        }

        if(Hash.GetValueOrDefault("cost") != null){
            Item.Cost = Decimal.Parse(Hash["cost"].ToString());
        }

        if(Hash.GetValueOrDefault("id") != null){
            Item.Id = Int32.Parse(Hash["id"].ToString());
        }
    }
}