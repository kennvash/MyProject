namespace MyStore.Models;

public class Customer
{
    public Int32 Id { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String Email { get; set; }
    public String Address { get; set; }
    public String ContactNumber { get; set; }

    public Customer(){}

    public Customer(string firstName, string lastName, string email, string address, string contactNumber)
    {
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Address = address;
        this.ContactNumber = contactNumber;
        this.Email = email;
    }
}