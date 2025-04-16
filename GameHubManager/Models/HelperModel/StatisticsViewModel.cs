using GameHubManager.Models;

namespace GameHubManager.Models.HelperModel
{
    public class StatisticsViewModel
    {
        public List<ReservationModel> ReservationsByaCertainPeriod { get; set; }    
        public List<SaleModel> SalesByaCertainPeriod { get; set; }
        public List<ReservationModel> ReservationsByToday { get; set; }
        public List<SaleModel> SalesByToday { get; set; }
        public List<ReservationModel> ReservationsByCurrentMonth { get; set; }
        public List<SaleModel > SalesByCurrentMonth { get; set;}

        




    }
}
