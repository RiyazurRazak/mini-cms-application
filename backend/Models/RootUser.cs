using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace cms_api.Models
{
    public class RootUser
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string EmailAddress { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        [DefaultValue(false)]
        public bool isMfaEnabled { get; set; }

    }
}
