using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HaitianRestaurantApp2.Models
{
    //The Order Header is the Order Summary
    public class OrderHeader
    {
        [Key]
        [Display(Name="Order Number")]
        public int OrderHeaderId { get; set; }

        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Required]
        [Display(Name = "Order Total")]
        [Range(0, 999.99)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(5,2)")]
        public decimal OrderTotal { get; set; }

        [Required]
        [Display(Name = "Order Status")]
        public string OrderStatus { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

    }
}
