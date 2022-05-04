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

        public RepositoryContext _db;

        public RecipesController( RepositoryContext db)
        {           
            _db = db;
            
        }

       

        [HttpGet]// pobranie wszystkich recept
        public ActionResult<IEnumerable<RecipePublicListDto>> GetRecipes(bool GLUTEN, bool SHELLFISH, bool EGGS, bool FISH,
                                                                        bool PEANUTS, bool SOY, bool Lactose, bool CELERY, bool MUSTARD,
                                                                        bool SESAME, bool SULPHUR_DIOXIDE, bool LUPINE, bool MUSCLES, bool Vegan,
                                                                        bool Vege)
        {

            //stwożenie listy
            #region TWOZENIE LISTY
            var query = (from objRecipe in _db.Recipes
                        join objRecipeList in _db.RecipeList on
                        objRecipe.Id equals objRecipeList.RecipeId
                        join objAllergens in _db.Allergens on
                        objRecipe.AllergenId equals objAllergens.Id
                        join objTags in _db.Tags on
                        objRecipe.TagId equals objTags.Id
                         select new RecipeForFilterDto
                        {
                            Id = objRecipeList.RecipeId,
                            RecipeName = objRecipe.RecipeName,
                            Date = objRecipe.Date,
                            Rating = objRecipeList.Rating,
                             IfPublic = objRecipe.IfPublic,
                             Photo = objRecipe.Photo,
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
                             Vege = objTags.Vege
                         }).ToList().Where(model=>model.IfPublic==true);
            #endregion

            //Filtry
            #region FILTRY

            if (GLUTEN == true)
            {
                query = query.Where(x => x.GLUTEN == false).ToList();
            }
            if (SHELLFISH == true)
            {
                query = query.Where(x => x.SHELLFISH == false).ToList();
            }
            if (EGGS == true)
            {
                query = query.Where(x => x.EGGS == false).ToList();
            }
            if (FISH == true)
            {
                query = query.Where(x => x.FISH == false).ToList();
            }
            if (PEANUTS == true)
            {
                query = query.Where(x => x.PEANUTS == false).ToList();
            }
            if (SOY == true)
            {
                query = query.Where(x => x.SOY == false).ToList();
            }
            if (Lactose == true)
            {
                query = query.Where(x => x.Lactose == false).ToList();
            }
            if (CELERY == true)
            {
                query = query.Where(x => x.CELERY == false).ToList();
            }
            if (MUSTARD == true)
            {
                query = query.Where(x => x.MUSTARD == false).ToList();
            }
            if (SESAME == true)
            {
                query = query.Where(x => x.SESAME == false).ToList();
            }
            if (SULPHUR_DIOXIDE == true)
            {
                query = query.Where(x => x.SULPHUR_DIOXIDE == false).ToList();
            }
            if (LUPINE == true)
            {
                query = query.Where(x => x.LUPINE == false).ToList();
            }
            if (MUSCLES == true)
            {
                query = query.Where(x => x.MUSCLES == false).ToList();
            }
            if (Vegan == true)
            {
                query = query.Where(x => x.Vegan == false).ToList();
            }
            if (Vege == true)
            {
                query = query.Where(x => x.Vege == false).ToList();
            }
           
            #endregion

            //zgupowanie po id
            var group = query.GroupBy(x => x.Id);
            
            List<RecipePublicListDto> recipePublicListDto = new List<RecipePublicListDto>();
            int j = 0;
            int counter = 0;
            int ratingSum = 0;

            //pozbycię się powturek + wyliczenie oceny
            #region OBLICZENIE RATINGU
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
            #endregion
            


            return recipePublicListDto;//listaRecept;
        }
        //------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet("{id}")]//pobranie jednej recepty 
        public ActionResult<RecipeDto> GetRecipe(Guid id, string token)
        {
            //stwożenie listy
            #region TWOZENIE LISTY
            var listaRecept = (from objRecipe in _db.Recipes
                               join objRecipeList in _db.RecipeList on
                               objRecipe.Id equals objRecipeList.RecipeId
                               join objAllergens in _db.Allergens on
                               objRecipe.AllergenId equals objAllergens.Id
                               join objTags in _db.Tags on
                               objRecipe.TagId equals objTags.Id
                               select new RecipeDto
                               {
                                   Id = objRecipeList.RecipeId,
                                   UserId = objRecipe.UserId,
                                   ifPublic = objRecipe.IfPublic,
                                   RecipeName = objRecipe.RecipeName,
                                   Instruction = objRecipe.Instruction,
                                   Date = objRecipe.Date,
                                   Rating = objRecipeList.Rating,
                                   Photo = objRecipe.Photo,
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

                               }
                               ).ToList();//.Where(x => x.Id == id);
            #endregion
            //Przypisanie listy sładników do recept
            #region LISTA SKŁADNIKÓW
            var indigrientsList = (from objRecipe in _db.Recipes
                                   join objIngredients in _db.Ingredients on
                                   objRecipe.Id equals objIngredients.RecipeId
                                   select new IndigrentsForRecipeDto
                                   {
                                       Ingredient=objIngredients.Ingredient,
                                       RecipeId=objIngredients.RecipeId
                                   }).Where(x=>x.RecipeId==id).ToList();
            #endregion

            //Znalezienie konkretnej recepty
            RecipeDto recipe = listaRecept.First(model=>model.Id==id);

            //obliczenie ratingu recepty
            int counter = 0;
            int ratingSum = 0;
                foreach (var item2 in listaRecept)
                {
                    
                    if (item2.Rating != 0)
                        counter++;
                    ratingSum += item2.Rating;
                }
            //przypisanie obliczonego ratingu
            recipe.Rating = ratingSum/counter;

            //przypisanie listy składników do recepty
            recipe.Ingredients = indigrientsList;


            if (token==null)
            {
                if (recipe.ifPublic==true)
                {
                    return recipe;

                }
                return null;//zmienić na null
            }
            else
            {
                #region DEKODOWANIE TOKENA
                var stream = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                var x = tokenS.Claims.ElementAt(0).Value;
                #endregion
                //znalezienie usera na podstawie emaila z dekodera
                var userToken = _db.Users.Single(model => model.UserName == x);
                var r = _db.Recipes.Single(model => model.Id == id);
                if (r.UserId == userToken.Id)
                {
                    return recipe;

                }
                return null;//zmienić na null
            }
 
        }
        //------------------------------------------------------------------------------------------------------------------------------------
        [HttpGet("Random")]
        public ActionResult<RecipeDto> GetRandomRecipe()
        {
            //stwożenie listy
            #region TWOZENIE LISTY
            var listaRecept = (from objRecipe in _db.Recipes
                               join objRecipeList in _db.RecipeList on
                               objRecipe.Id equals objRecipeList.RecipeId
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
                                   Photo = objRecipe.Photo,
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

                               }
                               ).ToList();
            #endregion
            //Pobranie randomowego ele z listy
            Random r = new Random();
            int rInt = r.Next(0, listaRecept.Count);
            var randomRecipe = listaRecept.ElementAt(rInt);
            //Stwożenie listy składników dla wylosowanej recepty
            #region LISTA SKŁADNIKÓW
            var indigrientsList = (from objRecipe in _db.Recipes
                                   join objIngredients in _db.Ingredients on
                                   objRecipe.Id equals objIngredients.RecipeId
                                   select new IndigrentsForRecipeDto
                                   {
                                       Ingredient = objIngredients.Ingredient,
                                       RecipeId = objIngredients.RecipeId
                                   }).Where(x => x.RecipeId == randomRecipe.Id).ToList();
            #endregion

            //obliczenie ratingu recepty
            int counter = 0;
            int ratingSum = 0;
            foreach (var item2 in listaRecept)
            {

                if (item2.Rating != 0)
                    counter++;
                ratingSum += item2.Rating;
            }
            //przypisanie obliczonego ratingu
            randomRecipe.Rating = ratingSum / counter;

            //przypisanie listy składników do recepty
            randomRecipe.Ingredients = indigrientsList;
            return randomRecipe;

        }


    }
}
