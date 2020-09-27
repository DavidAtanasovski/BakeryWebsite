using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bakery.Models
{
    public class FoodDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Ingredients { get; set; }
        [Required]
        public int Calories { get; set; }
        [Required]
        public int Price { get; set; }
        public string Photo { get; set; }
    }
}
