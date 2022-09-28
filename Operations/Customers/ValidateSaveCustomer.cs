namespace MyStore.Operations.Customers;

public class ValidateSaveCustomer : Validator
{
    public Int32 Id { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    public String Address { get; set; }
    public String ContactNumber { get; set; }

    public void InitializeParameters(Dictionary<string, object> hash)
    {
        if(hash.GetValueOrDefault("id") != null){
            this.Id = Int32.Parse(hash["id"].ToString());
        }

        if(hash.GetValueOrDefault("firstName") != null){
            this.FirstName = hash["firstName"].ToString();
        }

        if(hash.GetValueOrDefault("lastName") != null){
            this.LastName = hash["lastName"].ToString();
        }

        if(hash.GetValueOrDefault("email") != null){
            this.Email = hash["email"].ToString();
        }

        if(hash.GetValueOrDefault("address") != null){
            this.Address = hash["address"].ToString();
        }

        if(hash.GetValueOrDefault("contactNumber") != null){
            this.ContactNumber = hash["contactNumber"].ToString();
        }
    }

    public override void run()
    {
        if(this.FirstName == null || this.FirstName.Equals("")){
            String msg = "First name is required!";
            this.AddError(msg, "firstName");
        }

        if(this.LastName == null || this.LastName.Equals("")){
            String msg = "Last name is required!";
            this.AddError(msg, "lastName");
        }

        if(this.Email == null || this.Email.Equals("")){
            String msg = "Email is required!";
            this.AddError(msg, "email");
        }

        if(this.Address == null || this.Address.Equals("")){
            String msg = "Address is required!";
            this.AddError(msg, "address");
        }

        if(this.ContactNumber == null || this.ContactNumber.Equals("")){
            String msg = "Contact number is required!";
            this.AddError(msg, "contactNumber");
        }
    }
}