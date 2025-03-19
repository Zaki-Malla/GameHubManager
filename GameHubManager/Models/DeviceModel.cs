using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameHubManager.Models
{
    public class DeviceModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الجهاز مطلوب")]
        [StringLength(100, ErrorMessage = "الاسم لا يمكن أن يتجاوز 100 حرف")]
        public string Name { get; set; }

        public int DeviceTypeId { get; set; }
        [ForeignKey("DeviceTypeId")]

        public virtual DeviceTypeModel DeviceType { get; set; }
        public virtual List<ReservationModel> Reservations { get; set; }
    }
}
