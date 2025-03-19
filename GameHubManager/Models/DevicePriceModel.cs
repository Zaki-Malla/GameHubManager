using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameHubManager.Models
{
    public class DevicePriceModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "نوع الجهاز مطلوب")]
        [Display(Name = "نوع الجهاز")]
        public int DeviceTypeId { get; set; }

        [Required(ErrorMessage = "السعر لكل ساعة مطلوب")]
        [Range(0.01, 1000, ErrorMessage = "السعر يجب أن يكون بين 0.01 و 1000")]
        [Display(Name = "السعر لكل ساعة")]
        public decimal PricePerHour { get; set; }

        [Display(Name = "نوع الجهاز")]
        [ForeignKey("DeviceTypeId")]
        public virtual DeviceTypeModel DeviceType { get; set; }
    }
}
