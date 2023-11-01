using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CBP.Models
{
    public class Gallery
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ValidateNever]
        public List<GalleryImage> GalleryImages { get; set; }

    }
}
