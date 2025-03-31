using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace GameHubManager.Models
{
    public class DSContext : IdentityDbContext<UserModel, RoleModel, int>
    {
        private readonly IConfiguration _configuration;

        public DSContext() { }

        public DSContext(DbContextOptions<DSContext> options, IConfiguration configuration)
            : base(options)
        {
            _configuration = configuration;
        }

        public virtual DbSet<UserModel> TbUser { get; set; }
        public virtual DbSet<RoleModel> TbRole { get; set; }
        public DbSet<DeviceModel> Devices { get; set; }
        public DbSet<DeviceTypeModel> DeviceTypes { get; set; }
        public DbSet<ReservationModel> Reservations { get; set; }
        public DbSet<DevicePriceModel> DevicePrices { get; set; }
        public DbSet<MenuItemModel> MenuItems { get; set; }
        public DbSet<SaleModel> MenuSales { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DeviceModel>()
        .HasOne(d => d.DeviceType)
        .WithMany(dt => dt.Devices)
        .HasForeignKey(d => d.DeviceTypeId)
        .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ReservationModel>()
                .HasOne(r => r.Device)
                .WithMany(d => d.Reservations)
                .HasForeignKey(r => r.DeviceId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}