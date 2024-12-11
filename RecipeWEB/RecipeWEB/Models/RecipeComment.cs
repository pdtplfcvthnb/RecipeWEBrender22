using System;
using System.Collections.Generic;

namespace RecipeWEB.Models;

public partial class RecipeComment
{
    public int CommentId { get; set; }

    public int RecipeId { get; set; }

    public int UserId { get; set; }

    public string Comment { get; set; } = null!;

    public DateTime? CommentDate { get; set; }

    public virtual Recipe Recipe { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
