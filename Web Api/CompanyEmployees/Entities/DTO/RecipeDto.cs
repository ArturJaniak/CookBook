using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class RecipeDto
    {
        public Guid Id { get; set; }
        public string Photo { get; set; }
        public string Instruction { get; set; }
        public Boolean IfPublic { get; set; }
        public DateTime Date { get; set; }
        public string UserId { get; set; }


    }
}
