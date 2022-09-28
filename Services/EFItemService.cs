namespace MyStore.Services;

using System.Collections.Generic;
using MyStore.Models;
using MyStore.Data;
using MyStore.Operations.Items;

public class EFItemService : IItemService
{
    private readonly DataContext _dataContext;

    public EFItemService(DataContext dataContext)
    {
        _dataContext = dataContext;
    }

    public bool Delete(int id)
    {
        Item item = this.FindById(id);
        if(item.Id != null && item.Id > 0){
            _dataContext.Items.Attach(item);
            _dataContext.Items.Remove(item);
            _dataContext.SaveChanges();
            return true;
        }
        return false;
    }

    public Item FindById(int id)
    {
        return _dataContext.Items.SingleOrDefault(i => i.Id == id);
    }

    public List<Item> GetAll()
    {
        return _dataContext.Items.ToList();
    }

    public Item Save(Item item)
    {
        if(item.Id == null || item.Id == 0){
            _dataContext.Items.Add(item);
        } else{
            Item temp = this.FindById(item.Id);
            temp.Name = item.Name;
            temp.Cost = item.Cost;
        }

        _dataContext.SaveChanges();

        return item;
    }

    public Item Save(Dictionary<string, object> hash)
    {
        var builder = new BuildItemFromHash(hash);
        builder.run();

        return this.Save(builder.Item);
    }
}