using GameHubManager.Models;
using GameHubManager.Models.HelperModel;

namespace GameHubManager.Repositories
{
    public interface IDevicePriceRepository
    {
        Task<List<DevicePriceModel>> GetAllDevicesPricesAsync();
        
        Task<DevicePriceModel> GetDevicesPricesByIdAsync(int id);
       
        Task AddDevicePriceAsync(DevicePriceModel deviceprice);
       
        Task UpdateDevicePriceAsync(DevicePriceModel deviceprice);
        
        Task DeleteDevicePriceAsync(int id);

    }
}