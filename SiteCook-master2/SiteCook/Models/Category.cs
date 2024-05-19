using System;
using System.Collections.Generic;

namespace SiteCook;

public partial class Category
{
    public int CategoryId { get; set; }

    public string NameCategory { get; set; } = null!;

    public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
}
