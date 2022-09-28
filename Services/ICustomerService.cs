namespace MyStore.Services;

using MyStore.Models;

public interface ICustomerService
{
    public List<Customer> GetAll();
    public Customer FindById(int id);
    public Customer Save(Customer c);
    public Customer Save(Dictionary<string, object> hash);
    public bool Delete(int id);
}