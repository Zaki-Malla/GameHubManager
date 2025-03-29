using GameHubManager.Models;
using GameHubManager.Models.HelperModel;
using Microsoft.EntityFrameworkCore;

namespace GameHubManager.Repositories
{
    public class DevicePriceRepository : IDevicePriceRepository
    {
        private readonly DSContext _context;

        public DevicePriceRepository(DSContext context)
        {
            _context = context;
        }

        public async Task<List<DevicePriceModel>> GetAllDevicesPricesAsync()
        {
            return await _context.DevicePrices
                .Include(p => p.DeviceType)
                .ToListAsync();
        }

        public async Task<DevicePriceModel> GetDevicesPricesByIdAsync(int id)
        {
            return await _context.DevicePrices
                .Include(p => p.DeviceType)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task<DevicePriceModel> GetDevicesPricesByTypeIdAsync(int typeId)
        {
            return await _context.DevicePrices
                .Include(p => p.DeviceType)
                .FirstOrDefaultAsync(p => p.DeviceTypeId == typeId);
        }

        public async Task AddDevicePriceAsync(DevicePriceModel deviceprice)
        {
            await _context.DevicePrices.AddAsync(deviceprice);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDevicePriceAsync(DevicePriceModel deviceprice)
        {
            _context.DevicePrices.Update(deviceprice);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDevicePriceAsync(int id)
        {
            var deviceprice = await _context.DevicePrices.FindAsync(id);
            if (deviceprice != null)
            {
                _context.DevicePrices.Remove(deviceprice);
                await _context.SaveChangesAsync();
            }
        }

    }
}
