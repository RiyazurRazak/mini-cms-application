using System.ComponentModel.DataAnnotations;

namespace cms_api.Models
{
    public class User
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string EmailAddress { get; set; }
    }
}
