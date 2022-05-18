using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class RecipeForFilterDto
    {
        public Guid Id { get; set; }//id recepty
        public string RecipeName { get; set; }
        public string Instruction { get; set; }
        public string UserId { get; set; }
        public bool IfPublic { get; set; }
        public string UserIdInList { get; set; }
        public DateTime Date { get; set; }
        public int Rating { get; set; }
        //Alergeny
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
        //------------------------------------------
        //Tagi
        public Boolean Vegan { get; set; }
        public Boolean Vege { get; set; }
        //------------------------------------------
        //Zdjęcie
        public string Photo { get; set; }//przerobić na liste
    }
}
