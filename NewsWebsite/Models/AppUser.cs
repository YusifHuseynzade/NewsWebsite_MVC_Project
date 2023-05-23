using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace NewsWebsite.Models
{
    public class AppUser : IdentityUser
    {
        [MaxLength(50)]
        public string FullName { get; set; }
    }
}
