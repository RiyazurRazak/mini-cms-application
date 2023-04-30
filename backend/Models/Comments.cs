using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_api.Models
{
    public class Comments
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Message { get; set; }

        [ForeignKey("User")]
        public string User_Id { get; set; }
        public User User { get; set; }

        [ForeignKey("Articles")]
        public string Article_Id { get; set; }
        public virtual Articles Articles { get; set; }

    }
}
