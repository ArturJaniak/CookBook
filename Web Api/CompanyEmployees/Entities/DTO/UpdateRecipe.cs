using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class UpdateRecipe
    {
        
        public IFormFile Photos { get; set; }
        //public bool tak { get; set; }
    }
}
