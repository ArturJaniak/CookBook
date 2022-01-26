using Contracts;
using Entities;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("api/recips")]
    public class RecipesController : ControllerBase
    {
        
        public RepositoryContext _db;
        public RecipesController( RepositoryContext db)
        {
            
            _db = db;
            
        }

        

        [HttpGet]
        public ActionResult<IEnumerable<RecipeDto>> GetRecipes()
        {
             List<Recipes>_recipes  = _db.Recipes.ToList();
            var x = _recipes
                    .Select(x => new RecipeDto
                    {
                        Id = x.Id,
                        Photo = x.Photo,
                        Instruction = x.Instruction,
                        IfPublic = x.IfPublic,
                        Date = x.Date,
                        UserId = x.UserId
                    }).ToList();
            return x.ToList();
        }
        [HttpGet("{id}")]
        public ActionResult<RecipeDto> GetRecipe(Guid id)
        {

            
            return Ok();
            
        }
        [HttpPost]
        public ActionResult CreateRecipe(CreateRecipeDto createRecipeDto)
        {
            var newID = Guid.NewGuid();
            //----------------------------------
            var myAllergens = new Allergens();
            myAllergens.Id = newID;

            myAllergens.CELERY = false;
            myAllergens.EGGS = false;
            myAllergens.FISH = false;
            myAllergens.GLUTEN = false;
            myAllergens.Lactose = false;
            myAllergens.LUPINE = false;
            myAllergens.MUSCLES = false;
            myAllergens.MUSTARD = false;
            myAllergens.PEANUTS = false;
            myAllergens.PEANUTS = false;
            myAllergens.SESAME = false;
            myAllergens.SHELLFISH = false;
            myAllergens.SOY = false;
            myAllergens.SULPHUR_DIOXIDE = false;
            //---------------------------------
            var myTags = new Tags();
            myTags.Id = newID;

            myTags.Vege = false;
            myTags.Vegan = false;
            //----------------------------------
            var myRecipe = new Recipes();
            myRecipe.Id = newID;
            //myRecipe.RecipeName = 
            //myRecipe.Photo = null;
            //myRecipe.Instruction = null;
            myRecipe.IfPublic = false;
            myRecipe.Date = DateTime.Now;
            myRecipe.AllergenId = newID;
            //myRecipe.IngredientId =
            myRecipe.TagId = newID;
            myRecipe.UserId = createRecipeDto.Id;

            
            _db.Allergens.Add(myAllergens);
            _db.Tags.Add(myTags);
            _db.Recipes.Add(myRecipe);
            _db.SaveChanges();  
            return Ok();
        }

        [HttpPost("delete")]
        public ActionResult DeleteConfirmed(DeleteRecipeDto deleteRecipeDto)
        {
            var id = deleteRecipeDto.RecipId;
            Recipes recipe = _db.Recipes.Find(id);
            _db.Recipes.Remove(recipe);
            Allergens allergens = _db.Allergens.Find(id);
            _db.Allergens.Remove(allergens);
            Tags tags = _db.Tags.Find(id);
            _db.Tags.Remove(tags);
            _db.SaveChanges();
            return Ok();
        }

    }
}
