using GameHubManager.Models;
using GameHubManager.Models.HelperModel;
using Microsoft.EntityFrameworkCore;

namespace GameHubManager.Repositories
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly DSContext _context;

        public ReservationRepository(DSContext context)
        {
            _context = context;
        }

        public async Task<List<ReservationModel>> GetAllReservationsAsync()
        {
            return await _context.Reservations
                .Include(p => p.Device)
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<ReservationModel> GetReservationsByIdAsync(int id)
        {
            return await _context.Reservations
                .Include(p => p.Device)
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddReservationAsync(ReservationModel reservation)
        {
            await _context.Reservations.AddAsync(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateReservationAsync(ReservationModel reservation)
        {
            _context.Reservations.Update(reservation);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteReservationAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);
            if (reservation != null)
            {
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
        }

    }
}