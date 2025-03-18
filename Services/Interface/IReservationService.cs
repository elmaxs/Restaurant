using Restaurant.Models;

namespace Restaurant.Services.Interface
{
    public interface IReservationService
    {
        Task<Reservation> CreateReservationAsync(Guid userId, Guid tableId, DateTime startTime, int durationHours);
        Task<Reservation?> GetReservationByIdAsync(Guid reservationId);
        Task<List<Reservation>> GetUserReservationsAsync(Guid userId);
        Task<bool> CancelReservationAsync(Guid reservationId, Guid userId);
        Task<List<Reservation>> GetReservationsByDateAsync(DateTime date);
    }
}
