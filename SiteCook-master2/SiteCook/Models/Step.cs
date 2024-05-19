using System;
using System.Collections.Generic;

namespace SiteCook;

public partial class Step
{
    public int StepId { get; set; }

    public int StepNumber { get; set; }

    public string? Description { get; set; }

    public byte[] Photo { get; set; } = null!;

    public int? RecipeId { get; set; }

    public virtual Recipe? Recipe { get; set; }
}
