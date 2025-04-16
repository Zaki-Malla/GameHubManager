using GameHubManager.Models;

namespace GameHubManager.Repositories
{
    public interface IStatisticsRepository
    {
        Task<List<ReservationModel>> GetReservationsByaCertainPeriod(DateTime FirstDate, DateTime SecondDate);
        Task<List<SaleModel>> GetSalesByaCertainPeriod(DateTime FirstDate, DateTime SecondDate);
        Task<List<ReservationModel>> GetReservationsByToday();
        Task<List<ReservationModel>> GetReservationsByCurrentMonth();
        Task<List<SaleModel>> GetSalesByToday();
        Task<List<SaleModel>> GetSalesByCurrentMonth();

    }
}
