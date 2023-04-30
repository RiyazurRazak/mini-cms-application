using System.ComponentModel.DataAnnotations;

namespace cms_api.Models
{
    public class Themes
    {
        [Key]
        public string Id { get; set; }
        public string Name { get; set; }
        public bool isActive { get; set; }

    }
}
