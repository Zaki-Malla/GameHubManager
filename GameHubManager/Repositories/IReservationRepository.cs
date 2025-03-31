using GameHubManager.Models;
using GameHubManager.Models.HelperModel;

namespace GameHubManager.Repositories
{
    public interface IReservationRepository
    {
        Task<List<ReservationModel>> GetAllReservationsAsync();

        Task<ReservationModel> GetReservationsByIdAsync(int id);

        Task AddReservationAsync(ReservationModel reservation);

        Task UpdateReservationAsync(ReservationModel reservation);

        Task DeleteReservationAsync(int id);
        Task<bool> IsDeviceReservedAsync(int deviceId);

    }
}