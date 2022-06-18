using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Ingredients
    {
        [Column("IngredientId")]
        public Guid Id { get; set; }

        public string Ingredient { get; set; }

        [ForeignKey(nameof(Recipes))]
        public Guid RecipeId { get; set; }
        public Recipes Recipes { get; set; }
        //public ICollection<Recipes> Recipes { get; set; }


    }
}
