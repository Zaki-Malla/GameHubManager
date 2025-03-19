using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameHubManager.Models
{
    public class UserModel : IdentityUser<int>
    {
        [Required]
        public string FullName { get; set; }
    }


}
