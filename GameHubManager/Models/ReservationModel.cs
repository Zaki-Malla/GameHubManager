using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GameHubManager.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [AllowNull]
        public DateTime? EndTime { get; set; }

        [AllowNull]
        public int? TotalMinutes { get; set; }

        [AllowNull]
        public int? NumberOfControllers { get; set; }

        [AllowNull]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? AmountDue { get; set; }

        [AllowNull]
        [Column(TypeName = "decimal(18,2)")]
        public decimal? AmountPaid { get; set; }

        [AllowNull]
        public int? GroupReservationId { get; set; }
        [ForeignKey("GroupReservationId")]

        public virtual GroupReservationModel GroupReservation { get; set; }

        [Required]
        public int DeviceId { get; set; }
        [ForeignKey("DeviceId")]

        public virtual DeviceModel Device { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public virtual UserModel User { get; set; }
    }
}
