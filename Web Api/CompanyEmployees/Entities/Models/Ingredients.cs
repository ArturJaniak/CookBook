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

        public string Ingredient1 { get; set; }
        public string Ingredient2 { get; set; }
        public string Ingredient3 { get; set; }
        public string Ingredient4 { get; set; }
        public string Ingredient5 { get; set; }
        public string Ingredient6 { get; set; }
        public string Ingredient7 { get; set; }
        public string Ingredient8 { get; set; }
        public string Ingredient9 { get; set; }
        public string Ingredient10 { get; set; }
        public string Ingredient11 { get; set; }
        public string Ingredient12 { get; set; }
        public string Ingredient13 { get; set; }
        public string Ingredient14 { get; set; }
        public string Ingredient15 { get; set; }
        public string Ingredient16 { get; set; }
        public string Ingredient17 { get; set; }
        public string Ingredient18 { get; set; }
        public string Ingredient19 { get; set; }
        public string Ingredient20 { get; set; }

        public ICollection<Recipes> Recipes { get; set; }


    }
}
