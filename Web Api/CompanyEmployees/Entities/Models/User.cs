using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public ICollection<RecipeList> RecipeList { get; set; }
        

        public Recipes Recipes { get; set; }
        public Ratings Ratings { get; set; }


    }
}
