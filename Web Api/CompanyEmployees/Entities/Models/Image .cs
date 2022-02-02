using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Image
    {
        [Column("ImageId")]
        public Guid Id { get; set; }
        public string ImageName { get; set; }
       // public byte[] ImageData { get; set; }
        public ICollection<ImageList> ImageList { get; set; }
    }
}
