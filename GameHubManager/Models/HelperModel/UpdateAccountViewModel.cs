using System.ComponentModel.DataAnnotations;

namespace GameHubManager.Models.HelperModel
{
    public class UpdateAccountViewModel
    {
        public string Option { get; set; } 
        public string NewEmail { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmPassword { get; set; }
    }
}
