using DTOs;
using HotelManagement.context;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;

public class ReportService : IReportService
{
    private readonly AppDbContext _context;

    public ReportService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<ReportDto> GenerateReportAsync(GenerateReportRequestDto request)
    {
        try
        {
            string reportData = "{}";

            if (request.ReportType.ToLower() == "occupancy")
            {
                int totalRooms = await _context.Rooms.CountAsync();
                int occupiedRooms = await _context.Rooms.CountAsync(r => r.Status == "Booked");
                double occupancyRate = totalRooms > 0 ? (double)occupiedRooms / totalRooms * 100 : 0;

                reportData = JsonConvert.SerializeObject(new
                {
                    TotalRooms = totalRooms,
                    OccupiedRooms = occupiedRooms,
                    OccupancyRate = $"{occupancyRate:F2}%"
                });
            }
            else if (request.ReportType.ToLower() == "revenue")
            {
                var totalRevenue = await _context.Payments
                    .Where(p => p.PaymentStatus == "Completed")
                    .SumAsync(p => p.Amount);

                var todayRevenue = await _context.Payments
                    .Where(p => p.PaymentStatus == "Completed" && p.PaymentDate.Date == DateTime.Today)
                    .SumAsync(p => p.Amount);

                reportData = JsonConvert.SerializeObject(new
                {
                    TotalRevenue = totalRevenue,
                    TodayRevenue = todayRevenue
                });
            }
            else if (request.ReportType.ToLower() == "kpi")
            {
                int totalReservations = await _context.Reservations.CountAsync();
                int cancelledReservations = await _context.Reservations.CountAsync(r => r.ReservationStatus == "Cancelled");

                double cancellationRate = totalReservations > 0
                    ? (double)cancelledReservations / totalReservations * 100
                    : 0;

                var avgStay = await _context.Reservations
                    .Where(r => r.CheckOutDate != null && r.CheckInDate != null)
                    .AverageAsync(r => EF.Functions.DateDiffDay(r.CheckInDate, r.CheckOutDate));

                reportData = JsonConvert.SerializeObject(new
                {
                    TotalReservations = totalReservations,
                    CancelledReservations = cancelledReservations,
                    CancellationRate = $"{cancellationRate:F2}%",
                    AverageStayLength = avgStay
                });
            }

            var report = new ReportModel
            {
                ReportType = request.ReportType,
                GeneratedByStaffId = request.StaffId,
                GeneratedDate = DateTime.Now,
                ReportData = reportData
            };

            _context.Reports.Add(report);
            await _context.SaveChangesAsync();

            return new ReportDto
            {
                ReportId = report.ReportId,
                ReportType = report.ReportType,
                GeneratedByStaffId = report.GeneratedByStaffId,
                GeneratedDate = report.GeneratedDate,
                ReportData = report.ReportData
            };
        }
        catch (Exception ex)
        {
            throw new Exception($"Error generating {request.ReportType} report: {ex.Message}", ex);
        }
    }

    public async Task<IEnumerable<ReportDto>> GetAllReportsAsync()
    {
        return await _context.Reports
            .Select(r => new ReportDto
            {
                ReportId = r.ReportId,
                ReportType = r.ReportType,
                GeneratedByStaffId = r.GeneratedByStaffId,
                GeneratedDate = r.GeneratedDate,
                ReportData = r.ReportData
            }).ToListAsync();
    }

    public async Task<ReportDto?> GetReportByIdAsync(int reportId)
    {
        var report = await _context.Reports.FindAsync(reportId);
        if (report == null) return null;

        return new ReportDto
        {
            ReportId = report.ReportId,
            ReportType = report.ReportType,
            GeneratedByStaffId = report.GeneratedByStaffId,
            GeneratedDate = report.GeneratedDate,
            ReportData = report.ReportData
        };
    }


     public async Task<IEnumerable<StaffDto>> GetAllStaffAsync()
    {
        try
        {   
            return await _context.Users
                .Select(u => new StaffDto
                {
                    UserId = u.UserId,
                    Name = u.Name
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving staff list: {ex.Message}", ex);
        }
    }
}
