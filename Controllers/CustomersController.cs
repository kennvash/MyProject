namespace MyStore.Controllers;

using Microsoft.AspNetCore.Mvc;
using MyStore.Models;
using MyStore.Services;
using MyStore.Operations;
using MyStore.Operations.Customers;
using System.Text.Json;
using MyStore.Filters;

[ApiController]
[Route("customers")]
//[ServiceFilter(typeof(AuthenticationFilter))] //Uncomment this line to test authentication
public class CustomersController : ControllerBase
{
    private readonly ILogger<CustomersController> _logger;
    private readonly ICustomerService _customerService;
    private readonly ValidateSaveCustomer _validateSaveCustomer;

    public CustomersController(ILogger<CustomersController> logger, ICustomerService customerService, ValidateSaveCustomer validateSaveCustomer)
    {
        _logger = logger;
        _customerService = customerService;
        _validateSaveCustomer = validateSaveCustomer;
    }

    [HttpGet]       
    public IActionResult Index()
    {
        List<Customer> customers = _customerService.GetAll();
        return Ok(customers);
    }

    [HttpGet("{id}")]
    public IActionResult Show(int id)
    {
        Customer customer = _customerService.FindById(id);
        _logger.LogInformation("Customer found: " + customer.Id);

        Validator validator = new ValidateGetCustomer(customer);
        validator.run();

        if(validator.HasErrors){
            return NotFound(validator.Payload);
        } else {
            return Ok(customer);
        }
    }

    [HttpPost]
    public IActionResult Create([FromBody]object payload)
    {
        try{
            Dictionary<string, object> hash = JsonSerializer.Deserialize<Dictionary<string, object>>(payload.ToString());

            _validateSaveCustomer.InitializeParameters(hash);
            _validateSaveCustomer.run();

            if(_validateSaveCustomer.HasErrors){
                return UnprocessableEntity(_validateSaveCustomer.Payload);
            } else {
                Customer tempCustomer = _customerService.Save(hash);

                hash["id"] = tempCustomer.Id;
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
            _validateSaveCustomer.InitializeParameters(hash);
            _validateSaveCustomer.run();

            if(_validateSaveCustomer.HasErrors){
                return UnprocessableEntity(_validateSaveCustomer.Payload);
            } else {
                return Ok(_customerService.Save(hash));
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
        Customer customer = _customerService.FindById(id);

        try{
            Validator validator = new ValidateGetCustomer(customer);
            validator.run();

            if(validator.HasErrors){
                return NotFound(validator.Payload);
            } else{
                return Ok(_customerService.Delete(id));
            }
        } catch (Exception e){
            Dictionary<string, object> error = new Dictionary<string, object>();
            error["msg"] = e.Message;

            return StatusCode(StatusCodes.Status500InternalServerError, error);
        }
    }
}