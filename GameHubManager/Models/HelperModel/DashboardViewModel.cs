using GameHubManager.Models;

namespace GameHubManager.Models.HelperModel
{
    public class DashboardViewModel
    {
        public List<DeviceViewModel> Devices { get; set; }
        public List<DeviceTypeModel> DeviceTypes { get; set; }
    }
}
