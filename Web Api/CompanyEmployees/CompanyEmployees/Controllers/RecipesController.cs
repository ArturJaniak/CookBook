using Contracts;
using Entities;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CompanyEmployees.Controllers
{
    [ApiController]
    [Route("api/recips")]
    public class RecipesController : ControllerBase
    {
        //private IUserService _userService;

        public RepositoryContext _db;
        //public AuthResponseDto _authResponse;

        public RecipesController( RepositoryContext db)
        {           
            _db = db;
            //_authResponse=authResponse;
        }
        //------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet]// pobranie wszystkich recept
        public ActionResult<IEnumerable<RecipePublicListDto>> GetRecipes()
        {
            //stwożenie listy
            var query = (from objRecipe in _db.Recipes
                        join objRecipeList in _db.RecipeList on
                        objRecipe.Id equals objRecipeList.RecipeId
                        join objImageList in _db.ImageList on
                        objRecipe.Id equals objImageList.RecipeId
                        join objImage in _db.Image on
                        objImageList.ImageId equals objImage.Id
                        select new RecipePublicListDto
                        {
                            Id = objRecipeList.RecipeId,
                            RecipeName = objRecipe.RecipeName,
                            Date = objRecipe.Date,
                            Rating = objRecipeList.Rating,
                            Photo = objImage.ImageName
                        }).ToList();
            //zgupowanie po id
            var group = query.GroupBy(x => x.Id);

            List<RecipePublicListDto> recipePublicListDto = new List<RecipePublicListDto>();
            int j = 0;
            int counter = 0;
            int ratingSum = 0;
            //pozbycię się powturek + wyliczenie oceny
            foreach (var item in group)
            {
                recipePublicListDto.Add(new RecipePublicListDto());
                foreach (var item2 in item)
                {                  
                    recipePublicListDto[j].RecipeName = item2.RecipeName;
                    recipePublicListDto[j].Id = item2.Id;
                    recipePublicListDto[j].Date = item2.Date;
                    recipePublicListDto[j].Photo = item2.Photo;
                    if (item2.Rating!=0)
                    counter++;
                    ratingSum+=item2.Rating;
                }
                if (counter != 0)
                    recipePublicListDto[j].Rating = ratingSum / counter;
                else
                    recipePublicListDto[j].Rating = 0;
                j++;
                counter = 0;
                ratingSum = 0;
            }

            return recipePublicListDto;//listaRecept;
        }
        //------------------------------------------------------------------------------------------------------------------------------------

        //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]//????????????????????????????
        [HttpGet("{id}")]//pobranie jednej recepty 
        public ActionResult<RecipeDto> GetRecipe(Guid id)
        {
            var listaRecept = (from objRecipe in _db.Recipes
                               join objRecipeList in _db.RecipeList on
                               objRecipe.Id equals objRecipeList.RecipeId
                               join objImageList in _db.ImageList on
                               objRecipe.Id equals objImageList.RecipeId
                               join objImage in _db.Image on
                               objImageList.ImageId equals objImage.Id
                               join objIngredients in _db.Ingredients on
                               objRecipe.Id equals objIngredients.RecipeId
                               join objAllergens in _db.Allergens on
                               objRecipe.AllergenId equals objAllergens.Id
                               join objTags in _db.Tags on
                               objRecipe.TagId equals objTags.Id
                               select new RecipeDto
                               {
                                   Id = objRecipeList.RecipeId,
                                   UserId = objRecipe.UserId,
                                   RecipeName = objRecipe.RecipeName,
                                   Instruction = objRecipe.Instruction,
                                   Date = objRecipe.Date,
                                   Rating = objRecipeList.Rating,
                                   Photo = objImage.ImageName,//zrobić liste
                                   GLUTEN = objAllergens.GLUTEN,
                                   SHELLFISH = objAllergens.SHELLFISH,
                                   EGGS = objAllergens.EGGS,
                                   FISH = objAllergens.FISH,
                                   PEANUTS = objAllergens.PEANUTS,
                                   SOY = objAllergens.SOY,
                                   Lactose = objAllergens.Lactose,
                                   CELERY = objAllergens.CELERY,
                                   MUSTARD = objAllergens.MUSTARD,
                                   SESAME = objAllergens.SESAME,
                                   SULPHUR_DIOXIDE = objAllergens.SULPHUR_DIOXIDE,
                                   LUPINE = objAllergens.LUPINE,
                                   MUSCLES = objAllergens.MUSCLES,
                                   Vegan = objTags.Vegan,
                                   Vege = objTags.Vege,

                                   //Ingredients = _db.Ingredients.Select(model => new IndigrentsForRecipeDto()//do poprawy żeby wypisywało tylko własne składniki
                                   //{
                                   //    RecipeId = model.RecipeId,
                                   //    Ingredient = model.Ingredient
                                   //}).ToList()
                               }
                               ).ToList();

            var indigrientsList = (from objRecipe in _db.Recipes
                                   join objIngredients in _db.Ingredients on
                                   objRecipe.Id equals objIngredients.RecipeId
                                   select new IndigrentsForRecipeDto
                                   {
                                       Ingredient=objIngredients.Ingredient,
                                       RecipeId=objIngredients.RecipeId
                                   }).Where(x=>x.RecipeId==id).ToList();

           

            RecipeDto recipe = listaRecept.First(model=>model.Id==id);
            recipe.Ingredients = indigrientsList;
            return recipe;
            
        }
        //------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet("Random")]
        public ActionResult<RecipeDto> GetRandomRecipe()
        {
            var listaRecept = (from objRecipe in _db.Recipes
                               join objRecipeList in _db.RecipeList on
                               objRecipe.Id equals objRecipeList.RecipeId
                               join objImageList in _db.ImageList on
                               objRecipe.Id equals objImageList.RecipeId
                               join objImage in _db.Image on
                               objImageList.ImageId equals objImage.Id
                               join objIngredients in _db.Ingredients on
                               objRecipe.Id equals objIngredients.RecipeId
                               join objAllergens in _db.Allergens on
                               objRecipe.AllergenId equals objAllergens.Id
                               join objTags in _db.Tags on
                               objRecipe.TagId equals objTags.Id
                               select new RecipeDto
                               {
                                   Id = objRecipeList.RecipeId,
                                   RecipeName = objRecipe.RecipeName,
                                   Instruction = objRecipe.Instruction,
                                   Date = objRecipe.Date,
                                   Rating = objRecipeList.Rating,
                                   Photo = objImage.ImageName,//zrobić liste
                                   GLUTEN = objAllergens.GLUTEN,
                                   SHELLFISH = objAllergens.SHELLFISH,
                                   EGGS = objAllergens.EGGS,
                                   FISH = objAllergens.FISH,
                                   PEANUTS = objAllergens.PEANUTS,
                                   SOY = objAllergens.SOY,
                                   Lactose = objAllergens.Lactose,
                                   CELERY = objAllergens.CELERY,
                                   MUSTARD = objAllergens.MUSTARD,
                                   SESAME = objAllergens.SESAME,
                                   SULPHUR_DIOXIDE = objAllergens.SULPHUR_DIOXIDE,
                                   LUPINE = objAllergens.LUPINE,
                                   MUSCLES = objAllergens.MUSCLES,
                                   Vegan = objTags.Vegan,
                                   Vege = objTags.Vege,

                                   //Ingredients = _db.Ingredients.Select(model => new IndigrentsForRecipeDto()//do poprawy żeby wypisywało tylko własne składniki
                                   //{
                                   //    RecipeId = model.RecipeId,
                                   //    Ingredient = model.Ingredient
                                   //}).ToList()
                               }
                               ).ToList();
            Random r = new Random();
            int rInt = r.Next(0, listaRecept.Count);
            var randomRecipe = listaRecept.ElementAt(rInt);
            var indigrientsList = (from objRecipe in _db.Recipes
                                   join objIngredients in _db.Ingredients on
                                   objRecipe.Id equals objIngredients.RecipeId
                                   select new IndigrentsForRecipeDto
                                   {
                                       Ingredient = objIngredients.Ingredient,
                                       RecipeId = objIngredients.RecipeId
                                   }).Where(x => x.RecipeId == randomRecipe.Id).ToList();



            RecipeDto recipe = listaRecept.First(model => model.Id == randomRecipe.Id);
            recipe.Ingredients = indigrientsList;
            return recipe;

        }


    }
}
