using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CBP.Models
{
    public class Dog
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Color { get; set; }
        [Required]
        public string Gender { get; set; }

        [Required]
        [Display(Name = "List Price")]
        [Range(1, 10000)]
        public double ListPrice { get; set; }
        [Required]
        [ValidateNever]
        public List<DogImage> DogImages { get; set; }

    }
}
