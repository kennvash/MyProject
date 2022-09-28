namespace MyStore.Operations.CartItems;

using MyStore.Models;

public class ValidateGetCartItem : Validator
{
    public CartItem CartItem { get; set; }

    public ValidateGetCartItem(CartItem cartItem)
    {
        CartItem = cartItem;
    }

    public override void run()
    {
        if(this.CartItem == null){
            String msg = "Cart Item not found!";
            this.AddError(msg, "cart item");
        }
    }
}