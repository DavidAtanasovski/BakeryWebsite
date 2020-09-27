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
    public class ListModel : PageModel
    {
        private readonly IFoodData foodData;

        [BindProperty(SupportsGet = true)]
        public string SearchTerm { get; set; }

        public IEnumerable<Food> Foods { get; set; }
        public ListModel (IFoodData foodData)
        {
            this.foodData = foodData;
        }

        public void OnGet()
        {
            Foods = foodData.GetFoods(SearchTerm);
        }

    }
}