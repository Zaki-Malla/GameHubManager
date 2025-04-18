using System.ComponentModel.DataAnnotations;

namespace GameHubManager.Models
{
    public class DeviceTypeModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الجهاز مطلوب")]
        [StringLength(100, ErrorMessage = "الاسم لا يمكن أن يتجاوز 100 حرف")]
        [Display(Name = "اسم الجهاز")]
        public string Name { get; set; }

        [Required(ErrorMessage = "مسار الصورة مطلوب")]
        [StringLength(255, ErrorMessage = "مسار الصورة لا يمكن أن يتجاوز 255 حرف")]
        [Display(Name = "صورة الجهاز")]
        public string ImagePath { get; set; }

        [Display(Name = "يحتوي على قبضات؟")]
        public bool HasControllers { get; set; }
        [Required]
        public bool IsActive { get; set; } = true;
        public virtual List<DeviceModel> Devices { get; set; }
        public virtual DevicePriceModel DevicePrice { get; set; }
    }
}
