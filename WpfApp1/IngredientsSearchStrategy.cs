using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes
{
    //Клас пошуку за інгрідієнтами успадкований від загальної стратегії пошуку
    public class IngredientsSearchStrategy : SearchStrategyBase
    {
        public override List<Recipe> Search(List<Recipe> recipes, string query)
        {
            //Розбиваємо запит на окремі інгрідієнти по ;
            List<string> ingredients = query.ToLower().Split(';').ToList();
            //Перевіряємо яи містить рецепт інгрідієнти з пошукового запиту
            return recipes.Where(recipe => ingredients.All(recipe.Ingredients.Contains)).ToList();
        }
    }
}
