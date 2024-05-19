using System;
using System.Collections.Generic;

namespace SiteCook;

public partial class Ingredient
{
    public int IngredientId { get; set; }

    public string IngredientName { get; set; } = null!;

    public double Proteins { get; set; }

    public double Carbs { get; set; }

    public double Fats { get; set; }

    public virtual ICollection<Gramm> Gramms { get; set; } = new List<Gramm>();
}
