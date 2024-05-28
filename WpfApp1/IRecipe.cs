using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Recipes
{
    public interface IRecipe
    {
        void AddRecipe(Recipe recipe);
        List<Recipe> SearchRecipes(string keyword);
        void LoadRecipesFromFile(string filePath);
        void SaveRecipesToFile(string filePath);
    }

    public class RecipeManager : IRecipe
    {
        private List<Recipe> recipes = new List<Recipe>();

        public void AddRecipe(Recipe recipe)
        {
            recipes.Add(recipe);
        }
        public List<Recipe> SearchRecipes(string keyword)
        {
            string lowerKeyword = keyword.ToLower();
            return recipes.FindAll(r =>
                r.Name.ToLower().Contains(lowerKeyword) ||
                r.Ingredients.Any(ingredient => ingredient.ToLower().Contains(lowerKeyword)) ||
                r.Instructions.ToLower().Contains(lowerKeyword) ||
                r.Type.ToLower().Contains(lowerKeyword) ||
                r.Occasions.Contains(lowerKeyword)
            );
        }

        public void LoadRecipesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] parts = line.Split('\t');
                if (parts.Length < 5)
                    continue;

                Recipe recipe = new Recipe
                {
                    Name = parts[0],
                    Type = parts[1],
                    Ingredients = parts[2].Split(';').ToList(),
                    Instructions = parts[3],
                    Occasions = parts[4]
                };
                AddRecipe(recipe);
            }
        }

        public void SaveRecipesToFile(string filePath)
        {
            List<string> lines = recipes.Select(recipe =>
                $"{recipe.Name}\t{recipe.Type}\t{string.Join(";",recipe.Ingredients.Select(i => i.ToLower()))}\t{recipe.Instructions.Replace("\n", " ").Trim().Replace("\r", " ")}\t{string.Join(";", recipe.Occasions)}"
            )
            .Where(line => !string.IsNullOrWhiteSpace(line)) // Видаляємо порожні рядки
            .ToList();

            File.WriteAllLines(filePath, lines);
        }

    }
}
