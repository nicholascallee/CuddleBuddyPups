using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBP.Models
{
    public class GalleryImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int GalleryId { get; set; }
        [ForeignKey("GalleryId")]
        [ValidateNever]
        public Gallery Gallery { get; set; }


    }
}
