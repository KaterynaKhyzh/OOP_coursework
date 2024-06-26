﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes
{
    public abstract class SearchStrategyBase : ISearchStrategy
    {
        public abstract List<Recipe> Search(List<Recipe> recipes, string query);
    }
}
