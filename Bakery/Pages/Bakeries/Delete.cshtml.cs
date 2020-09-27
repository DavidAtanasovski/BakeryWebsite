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
    public class DeleteModel : PageModel
    {
        private readonly IFoodData foodData;
        public Food Food { get; set; }
        public DeleteModel(IFoodData foodData)
        {
            this.foodData = foodData;
        }

        public IActionResult OnGet(int foodId)
        {
            Food = foodData.GetFoodById(foodId);
            if(Food== null)
            {
                return RedirectToPage("./NotFound");

            }
            return Page();
        }

        public IActionResult OnPost(int foodId)
        {
            var x = foodData.Delete(foodId);
            if(x == null)
            {
                return RedirectToPage("./NotFound");

            }
            foodData.Commit();
            return RedirectToPage("./List");
        }

    }
}