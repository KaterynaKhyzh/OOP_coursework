using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Recipes
{
    public partial class SearchResultWindow : Window
    {
        private List<Recipe> searchResults;
        private RecipeSelectionViewModel viewModel;
        private RecipeManager recipeManager;

        public SearchResultWindow(List<Recipe> results)
        {
            InitializeComponent();
            searchResults = results;
            viewModel = new RecipeSelectionViewModel(); // Ініціалізуємо об'єкт viewModel
            recipeManager = new RecipeManager(); // Ініціалізуємо об'єкт recipeManager
            DisplayResults();
        }

        private void DisplayResults()
        {
            dgSearchResults.Items.Clear();

            foreach (var recipe in searchResults)
            {
                dgSearchResults.Items.Add(recipe);
            }
        }
    }
}
