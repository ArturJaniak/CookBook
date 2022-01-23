using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Ratings
    {
        [Column("RatingId")]
        public Guid Id { get; set; }

        [Range(1,5, ErrorMessage ="Out of range 1-5")]
        public int Rating { get; set; }
        //public ICollection<Recipes> Recipes { get; set; }
        //------------------------------------
        [ForeignKey(nameof(Recipes))]
        public Guid RecipesId { get; set; }
        public Recipes Recipes { get; set; }
        //-----------------------------------
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public User User { get; set; }

    }
}
