using GameHubManager.Models;
using GameHubManager.Models.HelperModel;

namespace GameHubManager.Repositories
{
    public interface ISaleRepository
    {
        Task<List<SaleModel>> GetAllSalesAsync();

        Task<SaleModel> GetSalesByIdAsync(int id);

        Task AddSaleAsync(SaleModel sale);

        Task UpdateSaleAsync(SaleModel sale);

        Task DeleteSaleAsync(int id);

    }
}