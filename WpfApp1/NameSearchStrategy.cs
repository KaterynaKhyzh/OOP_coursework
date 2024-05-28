using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes
{
    public class NameSearchStrategy : SearchStrategyBase
    {
        public override List<Recipe> Search(List<Recipe> recipes, string query)
        {
            return recipes.Where(recipe => recipe.Name.ToLower().Contains(query.ToLower())).ToList();
        }
    }
}
