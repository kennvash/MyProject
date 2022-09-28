namespace MyStore.Operations.Customers;

using MyStore.Models;

public class BuildCustomerFromHash
{
    public Dictionary<string, object> Hash { get; set; }
    public Customer Customer { get; set; }

    public BuildCustomerFromHash(Dictionary<string, object> hash)
    {
        Hash = hash;
        Customer = new Customer();
    }

    public void run()
    {
        if(Hash.GetValueOrDefault("id") != null){
            Customer.Id = Int32.Parse(Hash["id"].ToString());
        }

        if(Hash.GetValueOrDefault("firstName") != null){
            Customer.FirstName = Hash["firstName"].ToString();
        }

        if(Hash.GetValueOrDefault("lastName") != null){
            Customer.LastName = Hash["lastName"].ToString();
        }

        if(Hash.GetValueOrDefault("email") != null){
            Customer.Email = Hash["email"].ToString();
        }

        if(Hash.GetValueOrDefault("address") != null){
            Customer.Address = Hash["address"].ToString();
        }

        if(Hash.GetValueOrDefault("contactNumber") != null){
            Customer.ContactNumber = Hash["contactNumber"].ToString();
        }
    }
}