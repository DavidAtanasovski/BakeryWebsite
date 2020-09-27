
using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data
{
    public interface IFoodData
    {
        Food GetFoodById(int foodId);
        Food Create(Food food);
        Food Delete(int foodId);
        IEnumerable<Food> GetFoods(string name = null);
        Food Update(Food food);
        int Commit();
    }
}
