using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Bakery.Pages.Bakeries
{
    public class EditModel : PageModel
    {
        private readonly IFoodData foodData;
        private readonly IWebHostEnvironment hostEnvironment;

        [BindProperty]
        public Food Food { get; set; }
        [BindProperty]
        public IFormFile Photo { get; set; }
        public EditModel(IFoodData foodData, IWebHostEnvironment hostEnvironment)
        {
            this.hostEnvironment = hostEnvironment;
            this.foodData = foodData;
        }

        public IActionResult OnGet(int? foodId)
        {
            if (foodId.HasValue)
            {
                Food = foodData.GetFoodById(foodId.Value);
                if(Food == null)
                {
                    return RedirectToPage("./NotFound");
                }

            }
            else
            {
                Food = new Food();
            }
            return Page();
        }


        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                if (Food.Id == 0 && Photo != null)
                {
                    Food.Photo = ProcessUploadedFile();
                    Food = foodData.Create(Food);
                }
                else
                {
                    Food.Photo = ProcessUploadedFile();
                    Food = foodData.Update(Food);
                }
                foodData.Commit();
                return RedirectToPage("./List", new { foodId = Food.Id });
            }
            return Page();
        }

        private string ProcessUploadedFile()
        {
            string uniqueFileName = null;
            if (Photo != null)
            {
                string uploadsFolder = Path.Combine(hostEnvironment.WebRootPath, "Photos");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + Photo.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    Photo.CopyTo(fileStream);
                }

            }

            return uniqueFileName;
        }
    }
}