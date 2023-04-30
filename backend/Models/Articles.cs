using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace cms_api.Models
{
    public class Articles
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Body { get; set; }

        [Required]
        public bool Status { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        public int Likes { get; set; }

        [ForeignKey("RootUser")]
        public string User_Id { get; set; }
        public RootUser User { get; set; }
        public ICollection<Comments>? Comments { get; set; }
    }
}
