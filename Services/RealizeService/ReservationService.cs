using Microsoft.EntityFrameworkCore;
using Restaurant.Data;
using Restaurant.Enums;
using Restaurant.Models;
using Restaurant.Services.Interface;

namespace Restaurant.Services.RealizeService
{
    public class ReservationService : IReservationService
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public ReservationService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<bool> CancelReservationAsync(Guid reservationId, Guid userId)
        {
            var reservation = await _applicationDbContext.Reservations.FindAsync(reservationId);
            if (reservation == null || reservation.ApplicationUserId != userId)
                return false; // Бронирование не найдено или не принадлежит пользователю

            if (reservation.Status == ReservationStatus.Cancelled)
                return false; // Уже отменено

            // Возврат предоплаты: 50%, если отменено более чем за 1 час
            if ((reservation.StartTime - DateTime.UtcNow).TotalHours > 1)
                reservation.PrepaymentAmount /= 2;
            else
                reservation.PrepaymentAmount = 0; // Если меньше 1 часа — возврат невозможен

            reservation.Status = ReservationStatus.Cancelled;
            await _applicationDbContext.SaveChangesAsync();
            return true;
        }

        public async Task<Reservation> CreateReservationAsync(Guid userId, Guid tableId, DateTime startTime, int durationHours)
        {
            var endTime = startTime.AddHours(durationHours);
            var table = await _applicationDbContext.Tables.FindAsync(tableId);
            if (table == null)
                throw new InvalidOperationException("Table is not found");

            bool isTableFree = !await _applicationDbContext.Reservations
                .AnyAsync(r => r.TableId == tableId &&
                            r.Status != ReservationStatus.Cancelled &&
                            ((r.EndTime > startTime) && (r.StartTime < endTime)));

            if (!isTableFree)
                throw new InvalidOperationException("Table is not free");

            var reservation = new Reservation
            {
                Id = Guid.NewGuid(),
                ApplicationUserId = userId,
                TableId = tableId,
                StartTime = startTime,
                DurationHours = durationHours,
                PrepaymentAmount = durationHours * 1000, // Цена: 1000 грн за час
                Status = ReservationStatus.Pending,
                CreatedAt = DateTime.UtcNow
            };

            await _applicationDbContext.Reservations.AddAsync(reservation);
            await _applicationDbContext.SaveChangesAsync();

            return reservation;
        }

        public async Task<Reservation?> GetReservationByIdAsync(Guid reservationId)
        {
            if (reservationId == Guid.Empty)
                throw new InvalidOperationException("Not correct data");

            return await _applicationDbContext.Reservations
                .Include(r => r.ApplicationUser)
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.Id == reservationId);
        }

        public async Task<List<Reservation>> GetReservationsByDateAsync(DateTime date)
        {
            return await _applicationDbContext.Reservations
                .Where(r => r.StartTime.Date == date)
                .Include(r => r.ApplicationUser)
                .Include(r => r.Table)
                .OrderBy(r => r.StartTime)
                .ToListAsync();
        }

        public async Task<List<Reservation>> GetUserReservationsAsync(Guid userId)
        {
            return await _applicationDbContext.Reservations
                .Where(r => r.ApplicationUserId == userId)
                .OrderByDescending(r => r.StartTime)
                .ToListAsync();
        }
    }
}
