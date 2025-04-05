using GameHubManager.Models.HelperModel;
using GameHubManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using GameHubManager.Repositories;

namespace GameHubManager.Controllers
{
    public class SaleController : Controller
    {
        private readonly UserManager<UserModel> _userManager;
        private readonly ISaleRepository _saleRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        public SaleController(ISaleRepository saleRepository, UserManager<UserModel> userManager, IMenuItemRepository menuItemRepository)
        {
            _userManager = userManager;
            _saleRepository = saleRepository;
            _menuItemRepository = menuItemRepository;
        }
        [HttpPost]
        public async Task<IActionResult> SellItems([FromBody] List<SaleItemModel> items)
        {
            var user = await _userManager.GetUserAsync(User);
            foreach (var item in items)
            {
               var theItem = await _menuItemRepository.GetMenuItemByIdAsync(item.ItemId);
                var sale = new SaleModel
                {
                    ItemName = item.ItemName,
                    Category = item.Category,
                    SoldPrice = item.Paid,
                    DuePrice = theItem.Price,
                    UserId = user.Id,
                    ReservationId = null,
                    SaleDate = DateTime.Now,
                    MenuItemId = item.ItemId,
                };
                await _saleRepository.AddSaleAsync(sale);
                theItem.Quantity -= 1;
                await _menuItemRepository.UpdateMenuItemAsync(theItem);
            }

            return Ok();
        }
    }
}
