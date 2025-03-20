using GameHubManager.Models;
using GameHubManager.Models.HelperModel;

namespace GameHubManager.Repositories
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItemModel>> GetAllMenuItemsAsync();

        Task<MenuItemModel> GetMenuItemsByIdAsync(int id);

        Task AddMenuItemAsync(MenuItemModel menuItem);

        Task UpdateMenuItemAsync(MenuItemModel menuItem);

        Task DeleteMenuItemAsync(int id);

    }
}