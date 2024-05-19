using System;
using System.Collections.Generic;

namespace SiteCook;

public partial class Gramm
{
    public int GrammId { get; set; }

    public int? RecipeId { get; set; }

    public int? IngredientId { get; set; }

    public virtual Ingredient? Ingredient { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
