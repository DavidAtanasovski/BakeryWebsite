using Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Data
{
    public class FoodDataSQL : IFoodData
    {
        private readonly FoodDbContext FoodDbContext;
        public FoodDataSQL(FoodDbContext FoodDbContext)
        {
            this.FoodDbContext = FoodDbContext;
        }

        public int Commit()
        {
            return FoodDbContext.SaveChanges();
        }

        public Food Create(Food food)
        {
            FoodDbContext.Foods.Add(food);
            return food;
        }

        public Food Delete(int foodId)
        {
            var tempFood = FoodDbContext.Foods.SingleOrDefault(r => r.Id == foodId);
            if (tempFood != null)
            {
                FoodDbContext.Foods.Remove(tempFood);

            }
            return tempFood;
        }

        public Food GetFoodById(int foodId)
        {
            return FoodDbContext.Foods.SingleOrDefault(r => r.Id == foodId);
        }

        public IEnumerable<Food> GetFoods(string name = null)
        {
            var param = !string.IsNullOrEmpty(name) ? $"{name}" : name;
            return FoodDbContext.Foods.Where(r => string.IsNullOrEmpty(name) || EF.Functions.Like(r.Name, param)).ToList();
        }

        public Food Update(Food food)
        {
            FoodDbContext.Entry(food).State = EntityState.Modified;
            return food;
        }
    }
}
