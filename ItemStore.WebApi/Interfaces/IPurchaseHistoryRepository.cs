using ItemStore.WebApi.csproj.Models.Entities;

namespace ItemStore.WebApi.csproj.Interfaces
{
    public interface IPurchaseHistoryRepository
    {
        Task<int> BuyItem(PurchaseHistory purchase);
    }
}