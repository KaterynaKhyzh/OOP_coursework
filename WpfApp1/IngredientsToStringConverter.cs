using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Windows.Data;

namespace Recipes
{
    //Клас реалізує інтерфейс IValueConverter, методи якого використовуються для перетворення даних у одну сторону і назад.
    public class IngredientsToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Gеревіряє, чи є value колекцією інгредієнтів
            if (value is IEnumerable<string> ingredients)
            {
                //Перетворює колекцію інгредієнтів у рядок, де інгредієнти розділені комами.
                return string.Join("; ", ingredients);
            }
            // Якщо колекція пуста, то повертаємо пустий рядок
            return value?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
