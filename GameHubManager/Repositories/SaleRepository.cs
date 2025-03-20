using GameHubManager.Models;
using GameHubManager.Models.HelperModel;
using Microsoft.EntityFrameworkCore;

namespace GameHubManager.Repositories
{
    public class SaleRepository : ISaleRepository
    {
        private readonly DSContext _context;

        public SaleRepository(DSContext context)
        {
            _context = context;
        }

        public async Task<List<SaleModel>> GetAllSalesAsync()
        {
            return await _context.MenuSales
                .Include(p => p.User)
                .ToListAsync();
        }

        public async Task<SaleModel> GetSalesByIdAsync(int id)
        {
            return await _context.MenuSales
                .Include(p => p.User)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddSaleAsync(SaleModel sale)
        {
            await _context.MenuSales.AddAsync(sale);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateSaleAsync(SaleModel sale)
        {
            _context.MenuSales.Update(sale);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteSaleAsync(int id)
        {
            var Sale = await _context.MenuSales.FindAsync(id);
            if (Sale != null)
            {
                _context.MenuSales.Remove(Sale);
                await _context.SaveChangesAsync();
            }
        }

    }
}