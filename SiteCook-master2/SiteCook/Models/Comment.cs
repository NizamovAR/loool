﻿using System;
using System.Collections.Generic;

namespace SiteCook;

public partial class Comment
{
    public int CommentId { get; set; }

    public string Text { get; set; } = null!;

    public int? RecipeId { get; set; }

    public int? UserId { get; set; }

    public virtual Recipe? Recipe { get; set; }

    public virtual User? User { get; set; }
}
