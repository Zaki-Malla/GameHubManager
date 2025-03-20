using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace GameHubManager.Models.HelperModel
{
    public class ChangeEmailPasswordModel
    {
        [AllowNull]
        [EmailAddress]
        public string NewEmail { get; set; }
        [AllowNull]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }
        [AllowNull]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }
        [AllowNull]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "كلمة المرور الجديدة وتأكيد كلمة المرور لا يتطابقان.")]
        public string ConfirmPassword { get; set; }

        public bool ChangeEmail { get; set; }
    }

}
