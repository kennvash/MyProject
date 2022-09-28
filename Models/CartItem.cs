namespace MyStore.Models;

public class CartItem
{
    public Int32 Id { get; set; }
    public Item Item { get; set; }
    public Int32 ItemId { get; set; }
    public Int32 Quantity { get; set; }
    public Decimal Cost { get; set; }
    public Customer Customer { get; set; }
    public Int32 CustomerId { get; set;}

    public CartItem(){}

    public CartItem(Item item, int quantity, decimal cost)
    {
        Item = item;
        Quantity = quantity;
        Cost = cost;
    }

    public CartItem(Item item, int quantity, decimal cost, Customer customer)
    {
        Item = item;
        Quantity = quantity;
        Cost = cost;
        Customer = customer;
        CustomerId = customer.Id;
    }
}