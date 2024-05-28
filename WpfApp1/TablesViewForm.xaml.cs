
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace Recipes
{
    public partial class TablesViewForm : Window
    {
        private const string TableFilePath = "tables.txt";
        private readonly ITable tableManager;

        public TablesViewForm(List<Table> tables)
        {
            InitializeComponent();
            tableManager = new TableManager();
            tableManager.LoadTablesFromFile(TableFilePath);
            UpdateTablesGrid();
        }

        private void UpdateTablesGrid()
        {
            var tables = tableManager.GetTables();

            dgTables.ItemsSource = tables.Select(t => new
            {
                t.Name, 
                Recipes = string.Join("; ", t.Recipes.Select(r => r.Name))
            }).ToList();
        }
    }
}
