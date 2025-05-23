﻿using GameHubManager.Models;
using GameHubManager.Models.HelperModel;

namespace GameHubManager.Repositories
{
    public interface IDeviceRepository
    {
        Task<List<DeviceViewModel>> GetAllDevicesWithReservationsAsync();

        Task<List<DeviceModel>> GetAllDevicesAsync();
        
        Task<DeviceModel> GetDevicesByIdAsync(int id);
       
        Task AddDeviceAsync(DeviceModel device);
       
        Task UpdateDeviceAsync(DeviceModel device);
       
        Task ToggleDeviceStatusAsync(int id);

    }
}