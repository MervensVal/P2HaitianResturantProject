using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace HaitianRestaurantApp2.Models
{
    public class Menu
    {
        [Key]
        public int MenuId { get; set; }


        [Required]
        [Display(Name = "Item")]
        [MaxLength(100)]
        public string ItemName { get; set; }


        [Required]
        [Display(Name = "Price")]
        [Range(0, 999.99)]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(5,2)")]
        public decimal Price { get; set; }


        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }


        [Display(Name = "Description")]
        [MaxLength(500)]
        public string Description { get; set; }
    }
}
