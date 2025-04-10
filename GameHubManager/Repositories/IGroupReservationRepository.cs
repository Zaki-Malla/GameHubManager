using GameHubManager.Models;

namespace GameHubManager.Repositories
{
    public interface IGroupReservationRepository
    {
        Task<GroupReservationModel> GetGroupReservationsByIdAsync(int id);
        Task AddGroupReservationAsync(GroupReservationModel groupReservation);
        Task UpdateReservationAsync(GroupReservationModel groupReservation);
        Task<List<GroupReservationModel>> GetActiveGroupReservationsAsync();

    }
}
