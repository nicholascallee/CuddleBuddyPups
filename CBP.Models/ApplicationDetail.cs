using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CBP.Models
{
    public class ApplicationDetail
    {
        [Key]
        public int Id { get; set; }
                   

        [ForeignKey("ApplicationHeaderId")]
        [ValidateNever]
        public ApplicationHeader ApplicationHeader { get; set; }

        [Required]
        
        public int ApplicationHeaderId { get; set; }


        public int DogId { get; set; }
        [ForeignKey("DogId")]
        [ValidateNever]
        public Dog Dog { get; set; }
    }
}
