using GameHubManager.Models;
using GameHubManager.Models.HelperModel;
using Microsoft.EntityFrameworkCore;

namespace GameHubManager.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
       private readonly DSContext _context;

       public DeviceRepository(DSContext context)
        {
            _context = context;
        }

       public async Task<List<DeviceModel>> GetAllDevicesAsync()
        {
            return await _context.Devices
                .Include(p => p.DeviceType)
                .Include(p => p.Reservations)
                .ToListAsync();
        }

       public async Task<DeviceModel> GetDevicesByIdAsync(int id)
        {
            return await _context.Devices
               .Include(p => p.DeviceType)
               .Include(p => p.Reservations)
               .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddDeviceAsync(DeviceModel device)
        {
            await _context.Devices.AddAsync(device);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeviceAsync(DeviceModel device)
        {
            _context.Devices.Update(device);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeviceAsync(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                _context.Devices.Remove(device);
                await _context.SaveChangesAsync();
            }
        }

    }
}
