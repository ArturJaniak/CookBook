using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Subs
    {
        public Guid Id { get; set; }

        [ForeignKey(nameof(Sub))]
        public string SubId { get; set; }
        public User Sub { get; set; }
    }
}
