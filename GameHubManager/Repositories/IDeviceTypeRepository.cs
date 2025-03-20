using GameHubManager.Models;
using GameHubManager.Models.HelperModel;

namespace GameHubManager.Repositories
{
    public interface IDeviceTypeRepository
    {
        Task<List<DeviceTypeModel>> GetAllDevicesTypesAsync();
       
        Task<DeviceTypeModel> GetDevicesTypesByIdAsync(int id);
        
        Task AddDeviceTypeAsync(DeviceTypeModel deviceType);
        
        Task UpdateDeviceTypeAsync(DeviceTypeModel deviceType);
        
        Task DeleteDeviceTypeAsync(int id);

    }
}