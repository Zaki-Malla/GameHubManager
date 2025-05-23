﻿using GameHubManager.Models;
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

        public async Task<List<DeviceViewModel>> GetAllDevicesWithReservationsAsync()
        {
            var devices = _context.Devices
        .Include(d => d.DeviceType)
            .ThenInclude(dt => dt.DevicePrice)
        .Include(d => d.Reservations)
        .OrderBy(d => d.DeviceType)
        .Select(d => new DeviceViewModel
        {
            Device = new DeviceModel
            {
                Id = d.Id,
                Name = d.Name,
                DeviceType = d.DeviceType,
                IsActive = d.IsActive,
                Reservations = d.Reservations
            },
            ActiveReservation = d.Reservations
                .FirstOrDefault(r => r.EndTime > DateTime.Now || r.EndTime == null)
        })
        .ToList();
            return devices;
        }

       public async Task<List<DeviceModel>> GetAllDevicesAsync()
        {
            return await _context.Devices
                .Include(p => p.DeviceType)
                    .ThenInclude(dt => dt.DevicePrice)
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

        public async Task ToggleDeviceStatusAsync(int id)
        {
            var device = await _context.Devices.FindAsync(id);
            if (device != null)
            {
                device.IsActive = !device.IsActive;
                _context.Devices.Update(device);
                await _context.SaveChangesAsync();
            }
        }

    }
}
