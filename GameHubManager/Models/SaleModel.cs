using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameHubManager.Models
{
    public class SaleModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "تاريخ البيع مطلوب")]
        [Display(Name = "تاريخ البيع")]
        public DateTime SaleDate { get; set; } = DateTime.Now;

        [Required(ErrorMessage = "اسم الوجبة مطلوب")]
        [StringLength(100, ErrorMessage = "الاسم لا يمكن أن يتجاوز 100 حرف")]
        [Display(Name = "اسم الوجبة")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "التصنيف مطلوب")]
        [StringLength(50, ErrorMessage = "التصنيف لا يمكن أن يتجاوز 50 حرف")]
        [Display(Name = "التصنيف")]
        public string Category { get; set; }

        [Required(ErrorMessage = "سعر البيع مطلوب")]
        [Range(0.01, 1000, ErrorMessage = "السعر يجب أن يكون بين 0.01 و 1000")]
        [Display(Name = "سعر البيع")]
        public decimal SoldPrice { get; set; }

        [Display(Name = "رقم الحجز")]
        public int? ReservationId { get; set; }

        [Display(Name = "الحجز")]
        public ReservationModel? Reservation { get; set; }

        [Required(ErrorMessage = "المستخدم مطلوب")]
        [Display(Name = "المستخدم")]
        public int UserId { get; set; }

        [Display(Name = "المستخدم")]
        [ForeignKey("UserId")]
        public virtual UserModel User { get; set; }
    }
}
