namespace MyStore.Services;

using System.Collections.Generic;
using MyStore.Models;
using MyStore.Data;
using MyStore.Operations.CartItems;

public class EFCartItemService : ICartItemService
{
    private readonly DataContext _dataContext;

    public EFCartItemService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public List<CartItem> GetAll()
    {
        return _dataContext.CartItems.ToList();
    }

    public CartItem SaveCartItem(CartItem cartItem)
    {
        if(cartItem.Id == null || cartItem.Id == 0){
            _dataContext.CartItems.Add(cartItem);
        } else{
            CartItem temp = this.FindById(cartItem.Id);
            temp.Item = cartItem.Item;
            temp.ItemId = cartItem.ItemId;
            temp.Quantity = cartItem.Quantity;
            temp.Cost = cartItem.Cost;
            temp.Customer = cartItem.Customer;
            temp.CustomerId = cartItem.CustomerId;
        }

        _dataContext.SaveChanges();

        return cartItem;
    }

    public bool ClearCartItems(int customerId)
    {
        List<CartItem> cartItems = this.GetCartItems(customerId);
        if(cartItems != null){
            foreach(var cartItem in cartItems){
                _dataContext.CartItems.Attach(cartItem);
                _dataContext.CartItems.Remove(cartItem);
                _dataContext.SaveChanges();
            }
            return true;
        }
        return false;
    }

    public List<CartItem> GetCartItems(int customerId)
    {
        return _dataContext.CartItems.Where(c => c.CustomerId == customerId).ToList();
    }

    public List<CartItem> UpdateCartItems(int customerId)
    {
        throw new NotImplementedException();
    }

    public CartItem FindById(int id)
    {
        return _dataContext.CartItems.SingleOrDefault(c => c.Id == id);
    }

    public bool RemoveCartItem(int id)
    {
        CartItem cartItem = this.FindById(id);
        if(cartItem != null){
            _dataContext.CartItems.Attach(cartItem);
            _dataContext.CartItems.Remove(cartItem);
            _dataContext.SaveChanges();

            return true;
        }
        return false;
    }

    public CartItem SaveCartItem(Dictionary<string, object> hash)
    {
        var builder = new BuildCartItemFromHash(hash);
        builder.run();

        return this.SaveCartItem(builder.CartItem);
    }
}