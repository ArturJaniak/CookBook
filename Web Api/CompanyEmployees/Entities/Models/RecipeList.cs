using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class RecipeList
    {
        [Column("RecipeListId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Recipes))]
        public Guid RecipeId { get; set; }
        public Recipes Recipes { get; set; }
    }
}
