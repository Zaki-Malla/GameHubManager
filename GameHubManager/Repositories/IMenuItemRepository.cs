using GameHubManager.Models;
using GameHubManager.Models.HelperModel;

namespace GameHubManager.Repositories
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItemModel>> GetAllMenuItemsAsync(); 

        Task<MenuItemModel> GetMenuItemByIdAsync(int id);

        Task<MenuItemModel> GetMenuItemByNameAsync(string name);

        Task AddMenuItemAsync(MenuItemModel menuItem);

        Task UpdateMenuItemAsync(MenuItemModel menuItem);

        Task DeleteMenuItemAsync(int id);

    }
}