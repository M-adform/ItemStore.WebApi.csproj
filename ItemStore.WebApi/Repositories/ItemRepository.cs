using ItemStore.WebApi.csproj.Contexts;
using ItemStore.WebApi.Interfaces;
using ItemStore.WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace ItemStore.WebApi.Repositories
{
    public class ItemRepository : IItemRepository
    {
        private readonly DataContext _dataContext;

        public ItemRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<Item>> GetItems()
        {
            return await _dataContext.Items.ToListAsync();
        }

        public async Task<Item?> GetItemById(Guid id)
        {
            return await _dataContext.Items.FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Item?> GetItemByName(string name)
        {
            return await _dataContext.Items.FirstOrDefaultAsync(i => i.Name == name);
        }

        public async Task<Guid> AddItem(Item item)
        {
            _dataContext.Items.Add(item);
            await _dataContext.SaveChangesAsync();
            return item.Id;
        }

        public async Task UpdateItemById(Guid id, Item item)
        {
            var itemToUpdate = await _dataContext.Items.FirstOrDefaultAsync(i => i.Id == id);

            if (itemToUpdate == null)
                return;

            itemToUpdate.Name = item.Name;
            itemToUpdate.Price = item.Price;

            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteItemById(Guid id)
        {
            var itemToDelete = await _dataContext.Items.FirstOrDefaultAsync(i => i.Id == id);

            if (itemToDelete == null)
                return;

            _dataContext.Remove(itemToDelete);
            await _dataContext.SaveChangesAsync();

        }

        //public List<GetItemResponse> GetItems()
        //{
        //    string query = "SELECT name, price FROM items";
        //    var response = _dataContext.Query<GetItemResponse>(query);

        //    return response.ToList();
        //}

        //public GetItemResponse GetItemById(Guid id)
        //{
        //    string query = "SELECT name, price FROM items WHERE id = @id";
        //    var queryArguments = new { id };

        //    return _dataContext.QuerySingle<GetItemResponse>(query, queryArguments);
        //}

        //public Guid AddItem(AddItemRequest item)
        //{
        //    string query = "INSERT INTO items (name, price) VALUES (@name, @price) RETURNING id";
        //    var queryArguments = new { name = item.Name, price = item.Price };

        //    return _dataContext.QuerySingle<Guid>(query, queryArguments);
        //}

        //public Guid UpdateItemById(UpdateItemRequest item, Guid id)
        //{
        //    string query = "UPDATE items SET name = @name, price = @price WHERE id = @id RETURNING id";
        //    var queryArguments = new
        //    {
        //        id,
        //        name = item.Name,
        //        price = item.Price
        //    };

        //    return _dataContext.QueryFirst<Guid>(query, queryArguments);
        //}

        //public void DeleteItemById(Guid id)
        //{
        //    string query = "DELETE FROM items WHERE id = @id";
        //    var queryArguments = new { id };
        //    _dataContext.Execute(query, queryArguments);
        //}
    }
}
