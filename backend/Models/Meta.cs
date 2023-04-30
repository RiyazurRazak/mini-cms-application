using System.ComponentModel.DataAnnotations;

namespace cms_api.Models
{
    public class Meta
    {
        [Key]
        public int Id { get; set; }
        public string BrandName {  get; set; }
        public string BrandDescription { get; set; }


    }
}
