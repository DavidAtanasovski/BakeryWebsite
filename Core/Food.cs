using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Core
{
    public class Food
    {
        public int Id { get; set; }
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
