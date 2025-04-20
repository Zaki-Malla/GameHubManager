using GameHubManager.Models;
using System.Reflection.Metadata;

namespace GameHubManager.Models.HelperModel
{
    public class GeneralStatisticsViewModel
    {
        public List<(DateTime Date, string DayName, decimal Total)> SalesByDay { get; set; }
        public List<(DateTime Date, string DayName, decimal Total)> ReservationsByDay { get; set; }
        public List<(DateTime Date, string DayName, decimal Total)> TotalFundByDay { get; set; }
        public decimal TotalWeeklyPaidSales { get; set; }

        public List<(string TypeName, decimal Total)> WeeklyPaidSalesByType { get; set; }
        public decimal TotalWeeklyDueSales { get; set; }

        public List<(string TypeName, decimal Total)> WeeklyDueSalesByType { get; set; }

        public decimal TotalDailyPaidSales { get; set; }

        public List<(string TypeName, decimal Total)> DailyPaidSalesByType { get; set; }
        public decimal TotalDailyDueSales { get; set; }

        public List<(string TypeName, decimal Total)> DailyDueSalesByType { get; set; }

        public decimal TotalDailyPaidReservations { get; set; }
        public List<(string TypeName, decimal Total)> DailyPaidReservationsByDevice { get; set; }

        public decimal TotalDailyDueReservations { get; set; }
        public List<(string TypeName, decimal Total)> DailyDueReservationsByDevice { get; set; }

        public decimal TotalMonthlyPaidReservations { get; set; }
        public List<(string TypeName, decimal Total)> MonthlyPaidReservationsByDevice { get; set; }
        public decimal TotalMonthlyDueReservations { get; set; }
        public List<(string TypeName, decimal Total)> MonthlyDueReservationsByDevice { get; set; }


    }
}
