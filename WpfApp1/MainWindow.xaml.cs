using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Recipes
{
    public partial class MainWindow : Window
    {
        private const string RecipeFilePath = "recipes.txt";
        private RecipeManager recipeManager = new RecipeManager();
        private TableManager tableManager = new TableManager();
        private Menu menu;
        private List<Recipe> allRecipes = new List<Recipe>();

        public MainWindow()
        {
            InitializeComponent();
            menu = new Menu();
            recipeManager.LoadRecipesFromFile(RecipeFilePath);
            allRecipes = recipeManager.SearchRecipes(string.Empty);
        }

        private void AddRecipeToFile(Recipe recipe)
        {
            recipeManager.AddRecipe(recipe);
            recipeManager.SaveRecipesToFile(RecipeFilePath);
        }

        private void btnAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            string name = txtName.Text;
            string type = cmbDishType.Text;
            string ingredientsText = txtIngredients.Text;
            List<string> ingredients = ingredientsText
        .Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries)
        .Select(ingredient => ingredient.Replace(";", "").Trim())
        .Where(ingredient => !string.IsNullOrWhiteSpace(ingredient))
        .ToList();
            string instructions = txtInstructions.Text;
            string occasions = cmbOccasion.Text;

            if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(type) ||
                string.IsNullOrWhiteSpace(ingredientsText) || string.IsNullOrWhiteSpace(instructions) ||
                string.IsNullOrWhiteSpace(occasions))
            {
                MessageBox.Show("Будь ласка, заповніть всі поля.", "Помилка");
                return;
            }

            Recipe newRecipe = new Recipe
            {
                Name = name,
                Type = type,
                Ingredients = ingredients,
                Instructions = instructions,
                Occasions = occasions
            };

            AddRecipeToFile(newRecipe);
            allRecipes.Add(newRecipe);
            ClearInputFields();
            MessageBox.Show("Рецепт успішно додано!", "Повідомлення");
        }

        private void ClearInputFields()
        {
            txtName.Clear();
            cmbDishType.SelectedIndex = -1;
            txtIngredients.Clear();
            txtInstructions.Clear();
            cmbOccasion.SelectedIndex = -1;
        }

        private void OpenRecipeSelectionForm_Click(object sender, RoutedEventArgs e)
        {
            RecipeSelectionForm recipeSelectionForm = new RecipeSelectionForm(allRecipes);
            recipeSelectionForm.Show();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            string searchTerm = txtSearch.Text;
            ISearchStrategy searchStrategy = null;

            ComboBoxItem selectedItem = cmbSearchStrategy.SelectedItem as ComboBoxItem;
            if (selectedItem != null)
            {
                string selectedStrategy = selectedItem.Content.ToString();

                // В залежності від обраного елемента створюємо відповідну стратегію
                if (selectedStrategy == "Шукати за назвою")
                {
                    searchStrategy = new NameSearchStrategy();
                }
                else if (selectedStrategy == "Шукати за інгредієнтами")
                {
                    searchStrategy = new IngredientsSearchStrategy();
                }
                else if (selectedStrategy == "Шукати за нагодою")
                {
                    searchStrategy = new OccasionSearchStrategy();
                }
                else if (selectedStrategy == "Шукати за типом")
                {
                    searchStrategy = new TypeSearchStrategy();
                }
            }

            // Перевіряємо, чи була обрана стратегія перед тим, як продовжити
            if (searchStrategy != null)
            {
                RecipeSearchEngine recipeSearch = new RecipeSearchEngine(searchStrategy);
                List<Recipe> searchResults = recipeSearch.SearchRecipes(allRecipes, searchTerm);
                SearchResultWindow resultsWindow = new SearchResultWindow(searchResults);
                resultsWindow.Show();
            }
            else
            {
                MessageBox.Show("Будь-ласка оберіть стратегію пошуку.", "Помилка");
            }
        }

        private void btnViewTables_Click(object sender, RoutedEventArgs e)
        {
            TablesViewForm tablesViewForm = new TablesViewForm(tableManager.GetTables());
            tablesViewForm.Show();
        }
    }
}
