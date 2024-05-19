using System;
using System.Collections.Generic;

namespace SiteCook;

public partial class Recipe
{
    public int RecipeId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int? UserId { get; set; }

    public byte[] Photo { get; set; } = null!;

    public int? CategoryId { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Favorite> Favorites { get; set; } = new List<Favorite>();

    public virtual ICollection<Gramm> Gramms { get; set; } = new List<Gramm>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Step> Steps { get; set; } = new List<Step>();

    public virtual User? User { get; set; }
}
