using Microsoft.EntityFrameworkCore;
using Models;


namespace HotelManagement.context;

public class AppDbContext : DbContext
{
  public AppDbContext(DbContextOptions options) : base(options)
  {

  }

  public DbSet<UserModel> Users { get; set; }
  public DbSet<GuestModel> Guests { get; set; }
  public DbSet<RoleModel> Roles { get; set; }
  public DbSet<RoomModel> Rooms { get; set; }

  public DbSet<ReservationModel> Reservations{ get; set; }

  public DbSet<PaymentModel> Payments { get; set; }

public DbSet<HouseKeepingTaskModel> HouseKeepingTasks { get; set; }

    public DbSet<MaintenanceRequestModel> MaintenanceRequests { get; set; }
    
    public DbSet<InventoryItemModel> InventoryItems { get; set; }

    public DbSet<ReportModel> Reports { get; set; }


  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<RoomModel>()
    .HasIndex(e => e.RoomNumber)
    .IsUnique();


    modelBuilder.Entity<MaintenanceRequestModel>(entity =>
       {
         entity.HasKey(m => m.MaintenanceRequestId);

         entity.Property(m => m.Description).HasMaxLength(500);

         entity.HasOne(m => m.Room)
                .WithMany()
                .HasForeignKey(m => m.RoomId)
                .OnDelete(DeleteBehavior.Restrict);

         entity.HasOne(m => m.AssignedToUser)
                .WithMany()
                .HasForeignKey(m => m.AssignedToUserId)
                .OnDelete(DeleteBehavior.SetNull);
       });
  }
}

