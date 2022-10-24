namespace MyStore.Controllers;

using Microsoft.AspNetCore.Mvc;
using MyStore.Models;
using MyStore.Services;
using MyStore.Operations;
using MyStore.Operations.CartItems;
using MyStore.Operations.Customers;
using MyStore.Operations.Items;
using System.Text.Json;

[ApiController]
[Route("cart_items")]
public class CartItemsController : ControllerBase
{
    private readonly ILogger<CartItemsController> _logger;
    private readonly ICartItemService _cartItemService;
    private readonly ValidateSaveCartItem _validateSaveCartItem;
    private readonly ICustomerService _customerService;
    private readonly IItemService _itemService;

    public CartItemsController(ILogger<CartItemsController> logger, ICartItemService cartItemService, ValidateSaveCartItem validateSaveCartItem,
     ICustomerService customerService, IItemService itemService)
    {
        _logger = logger;
        _cartItemService = cartItemService;
        _validateSaveCartItem = validateSaveCartItem;
        _customerService = customerService;
        _itemService = itemService;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<CartItem> cart = _cartItemService.GetAll();
        return Ok(cart);
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        CartItem cart = _cartItemService.FindById(id);

        Validator validator = new ValidateGetCartItem(cart);
        validator.run();

        if(validator.HasErrors){
            return NotFound(validator.Payload);
        } else {
            return Ok(cart);
        }
    }

    [HttpGet("customers/{id}")]
    public IActionResult GetCartItems(int id)
    {
        //id here is of customer id
        List<CartItem> cart = _cartItemService.GetCartItems(id);
        return Ok(cart);
    }

    [HttpPost]
    public IActionResult Create([FromBody]object payload)
    {
        try{
            CartItem cart = new CartItem();
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            _validateSaveCartItem.InitializeParameters(hash);
            _validateSaveCartItem.run();

            if(_validateSaveCartItem.HasErrors){
                return UnprocessableEntity(_validateSaveCartItem.Payload);
            } else {
                Customer customer = _customerService.FindById(Int32.Parse(hash["customerId"].ToString()));

                Validator validator = new ValidateGetCustomer(customer);
                validator.run();

                if(validator.HasErrors){
                    return NotFound(validator.Payload);
                } else {
                    Item item = _itemService.FindById(Int32.Parse(hash["itemId"].ToString()));
                    
                    Validator temp = new ValidateGetItem(item);
                    temp.run();

                    if(temp.HasErrors){
                        return NotFound(temp.Payload);
                    } else {
                        CartItem tempCart = _cartItemService.SaveCartItem(hash);
                        hash["id"] = tempCart.Id;
                        return Ok(hash);
                    }
                }
            }
        } catch (Exception e){
            Dictionary<string, object> error = new Dictionary<string, object>();
            error["msg"] = e.Message;

            return StatusCode(StatusCodes.Status500InternalServerError, error);
        }
    }

    [HttpPut("{id}")]
    public IActionResult Update([FromBody]object payload, int id)
    {
        try{
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            hash["id"] = id;
            _validateSaveCartItem.InitializeParameters(hash);
            _validateSaveCartItem.run();

            if(_validateSaveCartItem.HasErrors){
                return UnprocessableEntity(_validateSaveCartItem.Payload);
            } else {
                return Ok(_cartItemService.SaveCartItem(hash));
            }
        } catch (Exception e){
            Dictionary<string, object> error = new Dictionary<string, object>();
            error["msg"] = e.Message;

            return StatusCode(StatusCodes.Status500InternalServerError, error);
        }  
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        CartItem cart = _cartItemService.FindById(id);

        try{
            Validator validator = new ValidateGetCartItem(cart);
            validator.run();

            if(validator.HasErrors){
                return NotFound(validator.Payload);
            } else {
                return Ok(_cartItemService.RemoveCartItem(id));
            }
        } catch (Exception e){
            Dictionary<string, object> error = new Dictionary<string, object>();
            error["msg"] = e.Message;

            return StatusCode(StatusCodes.Status500InternalServerError, error);
        }
    }

    [HttpDelete("clear/{id}")]
    public IActionResult Clear(int id)
    {
        Customer customer = _customerService.FindById(id);

        try{
            Validator validator = new ValidateGetCustomer(customer);
            validator.run();

            if(validator.HasErrors){
                return NotFound(validator.Payload);
            } else {
                return Ok(_cartItemService.ClearCartItems(id));
            }
        } catch (Exception e){
            Dictionary<string, object> error = new Dictionary<string, object>();
            error["msg"] = e.Message;

            return StatusCode(StatusCodes.Status500InternalServerError, error);
        }        
    }
}