using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes
{
    public class RecipeSelectionViewModel
    {
        public List<string> DishTypes { get; set; }
        public List<string> Occasions { get; set; }
        public string SelectedDishType { get; set; }
        public string SelectedOccasion { get; set; }

        public RecipeSelectionViewModel()
        {
            DishTypes = new List<string> { "Салати", "М'ясні страви", "Супи", "Закуски", "Десерти", "Напої" };
            Occasions = new List<string> { "День народження", "Новий рік", "Хрестини", "Великдень", "Різдво", "Весілля" };
        }
    }
}
