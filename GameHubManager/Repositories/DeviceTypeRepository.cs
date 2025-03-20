using GameHubManager.Models;
using GameHubManager.Models.HelperModel;
using Microsoft.EntityFrameworkCore;

namespace GameHubManager.Repositories
{
    public class DeviceTypeRepository : IDeviceTypeRepository
    {
        private readonly DSContext _context;

        public DeviceTypeRepository(DSContext context)
        {
            _context = context;
        }

        public async Task<List<DeviceTypeModel>> GetAllDevicesTypesAsync()
        {
            return await _context.DeviceTypes
                .Include(p => p.DevicePrice)
                .Include(p => p.Devices)
                .ToListAsync();
        }

        public async Task<DeviceTypeModel> GetDevicesTypesByIdAsync(int id)
        {
            return await _context.DeviceTypes
                .Include(p => p.DevicePrice)
                .Include(p => p.Devices)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddDeviceTypeAsync(DeviceTypeModel deviceType)
        {
            await _context.DeviceTypes.AddAsync(deviceType);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeviceTypeAsync(DeviceTypeModel deviceType)
        {
            _context.DeviceTypes.Update(deviceType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeviceTypeAsync(int id)
        {
            var deviceType = await _context.DeviceTypes.FindAsync(id);
            if (deviceType != null)
            {
                _context.DeviceTypes.Remove(deviceType);
                await _context.SaveChangesAsync();
            }
        }

    }
}