using GameHubManager.Models;

namespace GameHubManager.Models.HelperModel
{
    public class DashboardViewModel
    {
        public List<DeviceViewModel> Devices { get; set; }
        public List<DeviceTypeModel> DeviceTypes { get; set; }
        public List<MenuItemModel> MenuItems { get; set; }
        public List<GroupReservationModel> activeGroupReservations { get; set; }
    }
}
