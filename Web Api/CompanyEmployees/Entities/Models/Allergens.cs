using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Allergens
    {
        [Column("AllergenId")]
        public Guid Id { get; set; }
        public string Alergen { get; set; }
        public ICollection<Recipes> Recipes { get; set; }

    }
}
