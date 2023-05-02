using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace cms_api.Models
{
    public class Pages
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string DomElements { get; set; }

        [Required]
        public string Slug { get; set; }

        public DateTime CreatedAt { get; set; }

        [DefaultValue(true)]
        public bool IsPublic { get; set; }

    }
}
