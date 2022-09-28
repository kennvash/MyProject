namespace MyStore.Operations.Customers;

using MyStore.Models;

public class ValidateGetCustomer : Validator
{
    public Customer Customer { get; set; }

    public ValidateGetCustomer(Customer customer)
    {
        Customer = customer;
    }

    public override void run()
    {
        if(this.Customer == null){
            String msg = "Customer not found!";
            this.AddError(msg, "customer");
        }
    }
}