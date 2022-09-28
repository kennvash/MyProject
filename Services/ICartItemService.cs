namespace MyStore.Services;

using MyStore.Models;

public interface ICartItemService
{
    public List<CartItem> GetCartItems(int customerId);
    public List<CartItem> UpdateCartItems(int customerId);
    public CartItem FindById(int id);
    public CartItem SaveCartItem(CartItem cartItem);
    public CartItem SaveCartItem(Dictionary<string, object> hash);
    public bool RemoveCartItem(int id);
    public bool ClearCartItems(int customerId);
    public List<CartItem> GetAll();
}