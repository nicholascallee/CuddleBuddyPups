using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace CBP.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        [Required]
        public int OrderHeaderId { get; set; }
        [ForeignKey("OrderHeaderId")]
        [ValidateNever]
        public OrderHeader OrderHeader { get; set; }
        [Required]
        public int DogId { get; set; }
        [ForeignKey("DogId")]
        [ValidateNever]
        public Dog Dog { get; set; }
        public int Count { get; set; }
        public double Price { get; set; }

    }
}
