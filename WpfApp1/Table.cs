using Recipes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes
{
    public class Table
    {
        public string Name { get; set; }
        public List<Recipe> Recipes { get; set; }
        public Table(string name)
        {
            Name = name;
            Recipes = new List<Recipe>();
        }
    }
}
