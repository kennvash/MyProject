namespace MyStore.Services;

using MyStore.Models;

public interface IItemService
{
    public List<Item> GetAll();
    public Item FindById(int id);
    public Item Save(Item item);
    public Item Save(Dictionary<string, object> hash);
    public bool Delete(int id);
}