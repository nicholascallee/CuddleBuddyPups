using System.ComponentModel.DataAnnotations;

namespace CBP.Models
{
    public class GalleryImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string ImageUrl { get; set; }

    }
}
