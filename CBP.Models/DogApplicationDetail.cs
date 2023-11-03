using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CBP.Models
{
    public class DogApplicationDetail
    {
        [Key]
        public int Id { get; set; }
        public int DogId { get; set; }
        [ForeignKey("DogId")]
        [ValidateNever]
        public Dog Dog { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        [ValidateNever]
        public ApplicationUser ApplicationUser { get; set; }

        public DateTime? ApplicationDate { get; set; }

        public string? ApplicationStatus { get; set; }
        public string? PaymentStatus { get; set; }

        public string? Answer1 { get; set; }
        public string? Answer2 { get; set; }
        public string? Answer3 { get; set; }
        public string? Answer4 { get; set; }
        public string? Answer5 { get; set; }


        [Required]
        public string Name { get; set; }

        [Display(Name = "Street Address")]
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        [Display(Name ="Zip Code")]

        public string? PostalCode { get; set; }
        [Display(Name = "Phone Number")]

        public string? PhoneNumber { get; set; }



    }
}
