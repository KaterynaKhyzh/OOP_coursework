using Recipes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes
{
    public interface ITable
    {
        List<Table> GetTables();
        void AddRecipeToTable(string tableName, Recipe recipe);
        void SaveTablesToFile(string filePath);
        void LoadTablesFromFile(string filePath);
    }

    internal class TableManager : ITable
    {
        private Dictionary<string, Table> tables = new Dictionary<string, Table>();

        public List<Table> GetTables()
        {
            return tables.Values.ToList();
        }

        public void AddRecipeToTable(string tableName, Recipe recipe)
        {
            if (!tables.ContainsKey(tableName))
            {
                tables[tableName] = new Table(tableName);
            }
            tables[tableName].Recipes.Add(recipe);

        }

        public void SaveTablesToFile(string filePath)
        {
            List<string> lines = new List<string>();
            foreach (var table in GetTables())
            {
                string recipes = string.Join("\t", table.Recipes.Select(r =>
                     $"{r.Name}\t{r.Type}\t{string.Join(";", r.Ingredients)}\t{r.Instructions.Replace("\n", " ").Replace("\r", " ")}\t{r.Occasions}"));
                // Додаємо рядок з назвою таблиці та її рецептами до списку
                lines.Add($"{table.Name}|{recipes}");
            }
            File.WriteAllLines(filePath, lines);
        }

        public void LoadTablesFromFile(string filePath)
        {
            if (!File.Exists(filePath))
                return;

            string[] lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split('|');
                if (parts.Length < 1)
                    continue;

                string tableName = parts[0];
                Table table = new Table(tableName);

                if (parts.Length == 2 && !string.IsNullOrWhiteSpace(parts[1]))
                {
                    string[] recipesData = parts[1].Split('\t');
                    for (int i = 0; i < recipesData.Length; i += 5)
                    {
                        if (i + 4 >= recipesData.Length)
                            continue;

                        Recipe recipe = new Recipe
                        {
                            Name = recipesData[i],
                            Type = recipesData[i + 1],
                            Ingredients = recipesData[i + 2].Split(';').ToList(),
                            Instructions = recipesData[i + 3],
                            Occasions = recipesData[i + 4]
                        };

                        table.Recipes.Add(recipe);
                    }
                }

                tables[tableName] = table;
            }
        }

    }
}

