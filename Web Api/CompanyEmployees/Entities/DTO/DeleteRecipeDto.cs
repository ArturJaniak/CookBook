using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class DeleteRecipeDto
    {
        public Guid RecipId { get; set; }
        public string token { get; set; }

    }
}
