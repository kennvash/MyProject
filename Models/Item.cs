namespace MyStore.Models;

public class Item
{
    public Int32 Id { get; set; }
    public String Name { get; set; }
    public Decimal Cost { get; set; }

    public Item(){}

    public Item(string name, decimal cost)
    {
        this.Name = name;
        this.Cost = cost;
    }
}