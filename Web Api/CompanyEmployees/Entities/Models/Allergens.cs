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
        public bool GLUTEN { get; set; }
        public bool SHELLFISH { get; set; }
        public bool EGGS { get; set; }
        public bool FISH { get; set; }
        public bool PEANUTS { get; set; }
        public bool SOY { get; set; }
        public bool Lactose { get; set; }
        public bool CELERY { get; set; }
        public bool MUSTARD { get; set; }
        public bool SESAME { get; set; }
        public bool SULPHUR_DIOXIDE { get; set; }
        public bool LUPINE { get; set; }
        public bool MUSCLES { get; set; }
        
        
        public ICollection<Recipes> Recipes { get; set; }

    }
}
