using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBP.Models
{
    public class DogImage
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int DogId { get; set; }
        [ForeignKey("DogId")]
        [ValidateNever]
        public Dog Dog { get; set; }

    }
}
