using ItemStore.WebApi.csproj.Contexts;
using ItemStore.WebApi.csproj.Interfaces;
using ItemStore.WebApi.csproj.Models.Entities;

namespace ItemStore.WebApi.csproj.Repositories
{
    public class PurchaseHistoryRepository : IPurchaseHistoryRepository
    {
        private readonly DataContext _dataContext;

        public PurchaseHistoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<int> BuyItem(PurchaseHistory purchase)
        {
            _dataContext.PurchaseHistories.Add(purchase);
            await _dataContext.SaveChangesAsync();
            return purchase.Id;
        }
    }
}