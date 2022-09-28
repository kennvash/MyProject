namespace MyStore.Services;

using System.Collections.Generic;
using MyStore.Models;
using MyStore.Data;
using MyStore.Operations.Customers;

public class EFCustomerService : ICustomerService
{
    private readonly DataContext _dataContext;

    public EFCustomerService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool Delete(int id)
    {
        Customer customer = this.FindById(id);
        if(customer.Id != null && customer.Id > 0){
            _dataContext.Customers.Attach(customer);
            _dataContext.Customers.Remove(customer);
            _dataContext.SaveChanges();
            return true;
        }
        return false;
    }

    public Customer FindById(int id)
    {
        return _dataContext.Customers.SingleOrDefault(c => c.Id == id);
    }

    public List<Customer> GetAll()
    {
        return _dataContext.Customers.ToList();
    }

    public Customer Save(Customer c)
    {
        if(c.Id == null || c.Id == 0){
            _dataContext.Customers.Add(c);
        } else{
            Customer temp = this.FindById(c.Id);
            temp.FirstName = c.FirstName;
            temp.LastName = c.LastName;
            temp.Address = c.Address;
            temp.ContactNumber = c.ContactNumber;
            temp.Email = c.Email;
        }

        _dataContext.SaveChanges();

        return c;
    }

    public Customer Save(Dictionary<string, object> hash)
    {
        var builder = new BuildCustomerFromHash(hash);
        builder.run();

        return this.Save(builder.Customer);
    }
}