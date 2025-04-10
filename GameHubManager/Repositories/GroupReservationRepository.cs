using GameHubManager.Models;
using Microsoft.EntityFrameworkCore;

namespace GameHubManager.Repositories
{
    public class GroupReservationRepository : IGroupReservationRepository
    {
        private readonly DSContext _context;
        public GroupReservationRepository(DSContext context)
        {
            _context = context; 
        }
        public async Task<GroupReservationModel> GetGroupReservationsByIdAsync(int id)
        {
            return await _context.GroupReservations
                .Include(p => p.Reservations)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
        public async Task AddGroupReservationAsync(GroupReservationModel groupReservation)
        {
            await _context.GroupReservations.AddAsync(groupReservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(GroupReservationModel groupReservation)
        {
            _context.GroupReservations.Update(groupReservation);
            await _context.SaveChangesAsync();
        }
        public async Task<List<GroupReservationModel>> GetActiveGroupReservationsAsync()
        {
            return await _context.GroupReservations
                                 .Where(r => r.EndTime == null || r.EndTime > DateTime.Now)
                                 .Include(r => r.Reservations)
                                 .ThenInclude(r => r.Device)
                                 .ThenInclude(r => r.DeviceType)
                                 .ThenInclude(r => r.DevicePrice)
                                 .Include(r => r.User)
                                 .ToListAsync();
        }

    }
}
