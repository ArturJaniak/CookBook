using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Models
{
    public class Tags
    {
        [Column("TagId")]
        public Guid Id { get; set; }
        public Boolean Vegan { get; set; }
        public Boolean Vege { get; set; }

        public ICollection<Recipes> Recipes { get; set; }




    }
}
