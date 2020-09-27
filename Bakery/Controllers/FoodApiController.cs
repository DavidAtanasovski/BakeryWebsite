using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Models;
using Core;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarWebApp.Controllers
{
    [Route("api/Foods")]
    [ApiController]
    public class FoodApiController : ControllerBase
    {
        private readonly IFoodData foodData;
        public FoodApiController (IFoodData foodData)
        {
            this.foodData = foodData;
        }

        [HttpGet]
        public IActionResult GetCarsAll()
        {
            var data = foodData.GetFoods();
            return Ok(data);
        }

        [HttpGet("{foodId}")]
        public IActionResult GetCar(int foodId)
        {
            var data = foodData.GetFoodById(foodId);
            if (data == null)
            {
                return NotFound();
            }
            return Ok(data);
        }

        [HttpPost]
        public IActionResult Create(FoodDto foodCreateDto)
        {
            if(foodCreateDto == null )
            {
                return BadRequest();
            }
            var food = new Food();
            food.Calories = foodCreateDto.Calories;
            food.Ingredients = foodCreateDto.Ingredients;
            food.Name = foodCreateDto.Name;
            food.Price = foodCreateDto.Price;
            food.Photo = foodCreateDto.Photo;

            foodData.Create(food);
            foodData.Commit();
            return CreatedAtRoute("GetFood", new { id = food.Id }, food);
            
        }

        [HttpDelete("{foodId}")]
        public IActionResult Delete(int foodId)
        {
            var temp = foodData.Delete(foodId);
            if (temp == null)
            {
                return BadRequest();
            }
            foodData.Commit();
            return NoContent();
        }
    }
}
