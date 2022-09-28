namespace MyStore.Models;

public class Order
{
    public Int32 Id { get; set; }
    public ICollection<Item> Items { get; set; }
    public Customer Customer { get; set; }
    public Int32 CustomerId { get; set; }
    public DateTime DateOrdered { get; set; }
    public Decimal TotalCost { get; set; }

    public Order(){}

    public Order(Customer customer, List<Item> items, Decimal totalCost)
    {
        this.Customer = customer;
        this.Items = items;
        this.TotalCost = totalCost;
        this.CustomerId = this.Customer.Id;
        this.DateOrdered = DateTime.Now;
    }
}