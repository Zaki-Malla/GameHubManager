using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace GameHubManager.Models
{
    public class GroupReservationModel
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime StartTime { get; set; }

        [AllowNull]
        public DateTime? EndTime { get; set; }

        [Required]
        public int TotalDevices { get; set; }

        [Required]
        public int UserId { get; set; }
        [ForeignKey("UserId")]

        public virtual UserModel User { get; set; }

        public virtual List<ReservationModel> Reservations { get; set; }

    }
}
