using System;
using System.Collections.Generic;

namespace Recipes
{
    //Інтерфейс визначає загальну структуру для будь-якої стратегії пошуку рецептів. 
    public interface ISearchStrategy
    {
        List<Recipe> Search(List<Recipe> recipes, string keyword);
    }
}
