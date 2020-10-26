using System.ComponentModel.DataAnnotations;

namespace Okta_Web.Models
{
    public class UserProfile
    {
        [Required]
        public string firstName { get; set; }
        [Required]
        public string lastName { get; set; }
        [Required]
        public string email { get; set; }
        public string login { get; set; }
        public string mobilePhone { get; set; }
        public string secondEmail { get; set; }
    }
}
