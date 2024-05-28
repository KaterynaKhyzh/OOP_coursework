using System.Collections.Generic;

namespace Recipes
{
    //Зміна стратегії пошуку
    public class RecipeSearchEngine
    {
        private ISearchStrategy _searchStrategy;

        public RecipeSearchEngine(ISearchStrategy searchStrategy)
        {
            _searchStrategy = searchStrategy;
        }

        public List<Recipe> SearchRecipes(List<Recipe> recipes, string query)
        {
            return _searchStrategy.Search(recipes, query);
        }
    }
}
