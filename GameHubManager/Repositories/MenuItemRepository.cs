using GameHubManager.Models;
using GameHubManager.Models.HelperModel;
using Microsoft.EntityFrameworkCore;

namespace GameHubManager.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly DSContext _context;

        public MenuItemRepository(DSContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItemModel>> GetAllMenuItemsAsync()
        {
            return await _context.MenuItems.ToListAsync();
        }

        public async Task<MenuItemModel> GetMenuItemByIdAsync(int id)
        {
            return await _context.MenuItems.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<MenuItemModel> GetMenuItemByNameAsync(string name)
        {
            return await _context.MenuItems.FirstOrDefaultAsync(p => p.Name == name);
        }

        public async Task AddMenuItemAsync(MenuItemModel menuItem)
        {
            await _context.MenuItems.AddAsync(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateMenuItemAsync(MenuItemModel menuItem)
        {
            _context.MenuItems.Update(menuItem);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteMenuItemAsync(int id)
        {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem != null)
            {
                _context.MenuItems.Remove(menuItem);
                await _context.SaveChangesAsync();
            }
        }

    }
}