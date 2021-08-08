using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HaitianRestaurantApp2.Models
{
    public class Location
    {
        [Key]
        public int LocationId { get; set; }

        [Required]
        [MaxLength(70, ErrorMessage = "Location Name can not be more than 70 characters")]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; }

        [Required]
        [MaxLength(35, ErrorMessage = "Street Address can not be more than 35 characters")]
        [Display(Name = "Street Address")]
        public string StreetAddress { get; set; }

        [Required]
        [MaxLength(35, ErrorMessage = "City can not be more than 35 characters")]
        [Display(Name = "City")]
        public string City { get; set; }

        [Required]
        [MaxLength(15, ErrorMessage = "State can not be more than 15 characters")]
        [Display(Name = "State")]
        public string State { get; set; }

        [Required]
        [MaxLength(5, ErrorMessage = "Zip Code can not be more than 5 characters")]
        [RegularExpression(@"^[0-9]{5}$", ErrorMessage = "Postal code must be numbers only and in ##### format.")]
        [Display(Name = "Zip Code")]
        public string ZipCode { get; set; }

        [Required]
        [RegularExpression("[0-9]{3}-[0-9]{3}-[0-9]{4}", ErrorMessage = "Phone number must be in ###-###-#### format")]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
