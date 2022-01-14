using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Recipes
    {
        [Column("RecipesId")]
        public Guid Id { get; set; }
        public string Photo { get; set; }
        
        public string Instruction { get; set; }

        public Boolean IfPublic { get; set; }

        public int RatingCounter { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey(nameof(Allergens))]
        public Guid AllergenId { get; set; }
        public Allergens Allergens { get; set; }
        //----------------------------------------
        [ForeignKey(nameof(Ingredients))]
        public Guid IngredientId { get; set; }
        public Ingredients Ingredients { get; set; }
        //----------------------------------------
        [ForeignKey(nameof(Ratings))]
        public Guid RatingId { get; set; }
        public Ratings Ratings { get; set; }
        //----------------------------------------
        [ForeignKey(nameof(Tags))]
        public Guid TagId { get; set; }
        public Tags Tags { get; set; }
        //----------------------------------------
        public ICollection<RecipeList> RecipeList { get; set; }




    }
}


