using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameHubManager.Models
{
    public class ReservationModel
    {
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; } = DateTime.Now;

        [Required]
        public DateTime? EndTime { get; set; }

        [Required]
        public int? TotalMinutes { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountDue { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal AmountPaid { get; set; }

        [Required]
        public int DeviceId { get; set; }
        [ForeignKey("DeviceId")]

        public virtual DeviceModel Device { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public virtual UserModel User { get; set; }
    }
}
