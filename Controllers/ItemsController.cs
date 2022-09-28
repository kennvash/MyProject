namespace MyStore.Controllers;

using Microsoft.AspNetCore.Mvc;
using MyStore.Models;
using MyStore.Services;
using MyStore.Operations;
using MyStore.Operations.Items;
using System.Text.Json;

[ApiController]
[Route("items")]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;
    private readonly IItemService _itemService;
    private readonly ValidateSaveItem _validateSaveItem;

    public ItemsController(ILogger<ItemsController> logger, IItemService itemService, ValidateSaveItem validateSaveItem)
    {
        _logger = logger;
        _itemService = itemService;
        _validateSaveItem = validateSaveItem;
    }

    [HttpGet]
    public IActionResult Index()
    {
        List<Item> items = _itemService.GetAll();
        return Ok(items);
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        Item item = _itemService.FindById(id);

        Validator validator = new ValidateGetItem(item);
        validator.run();

        if(validator.HasErrors){
            return NotFound(validator.Payload);
        } else {
            return Ok(item);
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody]object payload)
    {
        try{
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            _validateSaveItem.InitializeParameters(hash);
            _validateSaveItem.run();

            if(_validateSaveItem.HasErrors){
                return UnprocessableEntity(_validateSaveItem.Payload);
            } else {
                Item tempItem = _itemService.Save(hash);

                hash["id"] = tempItem.Id;
                return Ok(hash);
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
            _validateSaveItem.InitializeParameters(hash);
            _validateSaveItem.run();

            if(_validateSaveItem.HasErrors){
                return UnprocessableEntity(_validateSaveItem.Payload);
            } else {
                return Ok(_itemService.Save(hash));
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
        Item item = _itemService.FindById(id);

        try{
            Validator validator = new ValidateGetItem(item);
            validator.run();

            if(validator.HasErrors){
                return NotFound(validator.Payload);
            } else{
                return Ok(_itemService.Delete(id));
            }
        } catch (Exception e){
            Dictionary<string, object> error = new Dictionary<string, object>();
            error["msg"] = e.Message;

            return StatusCode(StatusCodes.Status500InternalServerError, error);
        }
    }
}