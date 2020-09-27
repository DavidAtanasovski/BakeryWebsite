using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakery.Pages.Bakeries
{
    public class DetailModel : PageModel
    {
        private readonly IFoodData foodData;
        public Food Food { get; set; }
        public DetailModel (IFoodData foodData)
        {
            this.foodData = foodData;
        }

        public IActionResult OnGet(int foodId)
        {
            Food = foodData.GetFoodById(foodId);
            if (Food == null)
            {

                return RedirectToPage("./NotFound");
            }
            return Page();
        }
    }
}