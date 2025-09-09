using HotelManagement.context;
using DTOs;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ReservationService : IReservationService
{
    private readonly AppDbContext _context;

    public ReservationService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<ReservationReadDto>> GetAllReservationsAsync()
    {
        try
        {
            return await _context.Reservations
                .Include(r => r.Room)
                .Select(r => new ReservationReadDto
                {
                    ReservationId = r.ReservationId,
                    GuestId = r.GuestId,
                    RoomId = r.RoomId,
                    RoomNo = r.Room.RoomNumber,
                    ReservationStatus = r.ReservationStatus,
                    PaymentStatus = r.PaymentStatus,
                    CheckInDate = r.CheckInDate,
                    CheckOutDate = r.CheckOutDate,
                    TotalAmount = r.TotalAmount
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching reservations: {ex.Message}");
        }
    }

    public async Task<ReservationReadDto?> GetReservationByIdAsync(int id)
    {
        try
        {
            var r = await _context.Reservations.FindAsync(id);
            if (r == null) return null;

            return new ReservationReadDto
            {
                ReservationId = r.ReservationId,
                GuestId = r.GuestId,
                RoomId = r.RoomId,
                ReservationStatus = r.ReservationStatus,
                PaymentStatus = r.PaymentStatus,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                TotalAmount = r.TotalAmount
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error fetching reservation with ID {id}: {ex.Message}");
        }
    }

    public async Task<ReservationReadDto> AddReservationAsync(ReservationCreateDto dto)
    {
        try
        {
            var room = await _context.Rooms.FindAsync(dto.RoomId);
            if (room == null) throw new Exception("Room not found");
            if (room.Status != "Available") throw new Exception("Room is not available for booking");

            var reservation = new ReservationModel
            {
                GuestId = dto.GuestId,
                RoomId = dto.RoomId,
                CreatedByUserId = dto.CreatedByUserId,
                CheckInDate = dto.CheckInDate,
                CheckOutDate = dto.CheckOutDate,
                TotalAmount = room.Price,
                ReservationStatus = "CheckedIn",
                PaymentStatus = "Pending"
            };

            room.Status = "Booked"; 

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return new ReservationReadDto
            {
                ReservationId = reservation.ReservationId,
                GuestId = reservation.GuestId,
                RoomId = reservation.RoomId,
                ReservationStatus = reservation.ReservationStatus,
                PaymentStatus = reservation.PaymentStatus,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                TotalAmount = room.Price
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error adding reservation: {ex.Message}");
        }
    }

    public async Task<ReservationReadDto?> UpdateReservationAsync(int id, ReservationUpdateDto dto)
    {
        try
        {
            var r = await _context.Reservations.FindAsync(id);
            if (r == null) return null;

            var room = await _context.Rooms.FindAsync(dto.RoomId);
            if (room == null) throw new Exception("Room not found");
            if (room.Status != "Available" && room.RoomId != r.RoomId)
                throw new Exception("New room is not available");

            r.CheckInDate = dto.CheckInDate;
            r.CheckOutDate = dto.CheckOutDate;
            r.TotalAmount = dto.TotalAmount;
            r.RoomId = dto.RoomId;

            await _context.SaveChangesAsync();

            return new ReservationReadDto
            {
                ReservationId = r.ReservationId,
                GuestId = r.GuestId,
                RoomId = r.RoomId,
                ReservationStatus = r.ReservationStatus,
                PaymentStatus = r.PaymentStatus,
                CheckInDate = r.CheckInDate,
                CheckOutDate = r.CheckOutDate,
                TotalAmount = r.TotalAmount
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error updating reservation: {ex.Message}");
        }
    }

    public async Task<string> CancelReservationAsync(int id)
    {
        try
        {
            var r = await _context.Reservations.FindAsync(id);
            if (r == null) return "Reservation cancelled successfully";

            if (r.ReservationStatus == "CheckedIn")
               return "Cannot cancel after check-in";
            
              if (r.ReservationStatus == "CheckedOut")
               return "The Reservation already Checked out";
               
                 if (r.ReservationStatus == "Cancelled")
                return "The Reservation already Cancelled";
            r.ReservationStatus = "Cancelled";
            var room = await _context.Rooms.FindAsync(r.RoomId);
            if (room != null) room.Status = "Available";

            await _context.SaveChangesAsync();
            return "Reservation cancelled successfully";
        }
        catch (Exception ex)
        {
            throw new Exception($"Error cancelling reservation: {ex.Message}");
        }
    }

    public async Task<string> CheckOutReservationAsync(int id)
  {
    try
    {
        var reservation = await _context.Reservations.FindAsync(id);
        if (reservation == null) return "Reservation not found";
            var payments = await _context.Payments.FirstOrDefaultAsync(m=>m.ReservationId == id);

            if (reservation.ReservationStatus == "Cancelled")
                return "Cannot check out after Cancelled";
            else if (reservation.ReservationStatus == "CheckedOut")
                return "Reservation already CheckedOut";
            else if (payments  == null)
                return "Prior to checking out, pay";

        reservation.ReservationStatus = "CheckedOut";

        var room = await _context.Rooms.FindAsync(reservation.RoomId);
        if (room != null)
        {
            room.Status = "Available";
        }

        await _context.SaveChangesAsync();
        return "Checked out successfully";
    }
    catch (Exception ex)
    {
        return $"Error during check-out: {ex.Message}";
    }
}
    
     public async Task<IEnumerable<GuestSummaryDto>> GetGuestsAsync()
    {
        try
        {
            return await _context.Guests
                .Select(g => new GuestSummaryDto
                {
                    GuestId = g.GuestId,
                    name = g.FirstName + " " + g.LastName
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving guests: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<RoomSummaryDto>> GetRoomsAsync()
    {
        try
        {
            return await _context.Rooms
                .Select(r => new RoomSummaryDto
                {
                    RoomId = r.RoomId,
                    RoomNumber = r.RoomNumber
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving rooms: {ex.Message}", ex);
        }
    }
}
