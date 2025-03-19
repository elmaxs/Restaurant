using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Models;
using Restaurant.Models.DTOs;
using Restaurant.Services.Interface;
using System.Security.Claims;

namespace Restaurant.Controllers
{
    [Authorize]
    [Route("api/reservation")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateReservationAsync([FromBody] ReservationRequest request)
        {
            if (request == null || request.DurationHours < 1)
                return BadRequest("Duration cant be less than 1");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            try
            {
                var reservation = await _reservationService.CreateReservationAsync(Guid.Parse(userId), request.TableId, request.StartTime, request.DurationHours);
                return Ok("Table successfully reserved");
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("cancel")]
        public async Task<IActionResult> CancelReservationAsync(Guid reservationId)
        {
            if (Guid.Empty == reservationId)
                return BadRequest("Not correct data");

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var result = await _reservationService.CancelReservationAsync(reservationId, Guid.Parse(userId));
            if (!result)
                return BadRequest("Reservation cant be canseled");

            return Ok("Table reserved successfully canceled");
        }

        [HttpGet("getById")]
        public async Task<IActionResult> GetReservationByIdAsync(Guid reservationId)
        {
            if (Guid.Empty == reservationId)
                return BadRequest("Not correct data");

            try
            {
                var reservation = await _reservationService.GetReservationByIdAsync(reservationId);
                return Ok(reservation);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("getByDate")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetReservationsByDateAsync(DateTime date)
        {
            var reservations = await _reservationService.GetReservationsByDateAsync(date);
            return Ok(reservations);
        }

        [HttpGet("getByUser")]
        public async Task<IActionResult> GetUserReservationsAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
                return Unauthorized();

            var reservations = await _reservationService.GetUserReservationsAsync(Guid.Parse(userId));
            if (reservations == null)
                return NotFound("Reservation not found");

            return Ok(reservations);
        }
    }
}
