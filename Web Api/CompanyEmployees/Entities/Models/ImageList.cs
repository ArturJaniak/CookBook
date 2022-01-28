using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class ImageList
    {
        [Column("ImageListId")]
        public Guid Id { get; set; }

        [ForeignKey(nameof(Recipes))]
        public Guid RecipeId { get; set; }
        public Recipes Recipes { get; set; }

        [ForeignKey(nameof(Image))]
        public Guid ImageId { get; set; }
        public Image Image { get; set; }
    }
}
