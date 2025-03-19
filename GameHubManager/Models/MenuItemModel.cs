using System.ComponentModel.DataAnnotations;

namespace GameHubManager.Models
{
    public class MenuItemModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "اسم الوجبة مطلوب")]
        [StringLength(100, ErrorMessage = "الاسم لا يمكن أن يتجاوز 100 حرف")]
        [Display(Name = "اسم الوجبة")]
        public string Name { get; set; }

        [Required(ErrorMessage = "التصنيف مطلوب")]
        [StringLength(50, ErrorMessage = "التصنيف لا يمكن أن يتجاوز 50 حرف")]
        [Display(Name = "التصنيف")]
        public string Category { get; set; }

        [Required(ErrorMessage = "الكمية مطلوبة")]
        [Range(0, int.MaxValue, ErrorMessage = "الكمية يجب أن تكون عدد صحيح غير سالب")]
        [Display(Name = "الكمية")]
        public int Quantity { get; set; }
    }
}
