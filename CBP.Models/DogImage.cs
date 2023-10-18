using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CBP.Models
{
    public class DogImage
    {
        public int Id { get; set; }
        [Required]
        public string ImageUrl { get; set; }
        public int DogId { get; set; }
        [ForeignKey("DogId")]
        public Dog Product { get; set; }

    }
}
