using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Recipes
{
    public partial class RecipeSelectionForm : Window
    {
        private readonly List<Recipe> recipes;
        private readonly RecipeSelectionViewModel viewModel = new RecipeSelectionViewModel();
        private readonly RecipeManager recipeManager;
        private const string TableFilePath = "tables.txt";
        private readonly TableManager tableManager;

        public RecipeSelectionForm(List<Recipe> recipes)
        {
            InitializeComponent();
            this.recipes = recipes;
            DataContext = viewModel;
            recipeManager = new RecipeManager();
            tableManager = new TableManager();
            tableManager.LoadTablesFromFile(TableFilePath);
            UpdateRecipesGrid();
            UpdateTableComboBox();
        }

        private void UpdateTableComboBox()
        {
            cmbTables.ItemsSource = tableManager.GetTables().Select(t => t.Name).ToList();
        }

        private void UpdateRecipesGrid()
        {
            var filteredRecipes = recipes;

            if (!string.IsNullOrEmpty(viewModel.SelectedDishType))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Type == viewModel.SelectedDishType).ToList();
            }

            if (!string.IsNullOrEmpty(viewModel.SelectedOccasion))
            {
                filteredRecipes = filteredRecipes.Where(r => r.Occasions.Contains(viewModel.SelectedOccasion)).ToList();
            }

            dgRecipes.ItemsSource = filteredRecipes;
        }

        private void cmbOccasion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecipesGrid();
        }

        private void AddToMenu_Click(object sender, RoutedEventArgs e)
        {
            Recipe selectedRecipe = dgRecipes.SelectedItem as Recipe;
            if (selectedRecipe != null)
            {
                string tableName = cmbTables.SelectedItem as string;
                tableManager.AddRecipeToTable(tableName, selectedRecipe);
                tableManager.SaveTablesToFile(TableFilePath); // Зберігаємо таблиці у файл
                MessageBox.Show($"Рецепт '{selectedRecipe.Name}' додано до столу '{tableName}'.", "Повідомлення");
                UpdateTableComboBox();
            }
            else
            {
                MessageBox.Show("Виберіть рецепт для додавання до столу.", "Помилка");
            }
        }

        private void cmbDishType_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {
            UpdateRecipesGrid();
        }
    }
}
