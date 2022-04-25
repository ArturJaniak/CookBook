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
    public class RecipeLogedViewController : Controller
    {
        public RepositoryContext _db;

        public RecipeLogedViewController(RepositoryContext db)
        {
            _db = db;
            //_authResponse=authResponse;
        }
        [HttpGet("GetMyList")]// pobranie wszystkich recept moich
        public ActionResult<IEnumerable<RecipePublicListDto>> GetMyList(string token, bool GLUTEN, bool SHELLFISH, bool EGGS, bool FISH,
                                                                        bool PEANUTS, bool SOY, bool Lactose, bool CELERY, bool MUSTARD,
                                                                        bool SESAME, bool SULPHUR_DIOXIDE, bool LUPINE, bool MUSCLES, bool Vegan,
                                                                        bool Vege)
        {
            if (token !=null)
            {
                //dekoder tokena
                #region DEKODER TOKENA
                var stream = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                #endregion

                //znalezienie elem mail
                var userMail = tokenS.Claims.ElementAt(0).Value;

                //wyszukanie odpowiedniego usera
                var user = _db.Users.Single(model => model.UserName == userMail);

                //stwożenie listy gdzie UserId == podanemu UserID
                #region TWOZENIE LISTY
                var query = (from objRecipe in _db.Recipes
                             join objRecipeList in _db.RecipeList on
                             objRecipe.Id equals objRecipeList.RecipeId
                             //join objImageList in _db.ImageList on
                             //objRecipe.Id equals objImageList.RecipeId
                             //join objImage in _db.Image on
                             //objImageList.ImageId equals objImage.Id
                             join objAllergens in _db.Allergens on
                             objRecipe.AllergenId equals objAllergens.Id
                             join objTags in _db.Tags on
                             objRecipe.TagId equals objTags.Id
                             select new RecipeForFilterDto
                             {
                                 Id = objRecipeList.RecipeId,
                                 UserId = objRecipe.UserId,
                                 RecipeName = objRecipe.RecipeName,
                                 Date = objRecipe.Date,
                                 Rating = objRecipeList.Rating,
                                 Photo = objRecipe.Photo,
                                 //Photo = objImage.ImageName,
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
                             }).Where(x=>x.UserId==user.Id).ToList();//.Where(model=>model.IfPublic==true)
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
                //query=Filter(filtersDto, query);
                #endregion
                //zgupowanie po id
                var group = query.GroupBy(x => x.Id);

                //twożenie zmiennych
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
                        if (item2.Rating != 0)
                            counter++;
                        ratingSum += item2.Rating;
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
            return null;
        }
        //------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet("GetSomeoneList")]// pobranie wszystkich recept kogoś
        public ActionResult<IEnumerable<RecipePublicListDto>> GetSomeoneList(string id, bool GLUTEN, bool SHELLFISH, bool EGGS, bool FISH,
                                                                        bool PEANUTS, bool SOY, bool Lactose, bool CELERY, bool MUSTARD,
                                                                        bool SESAME, bool SULPHUR_DIOXIDE, bool LUPINE, bool MUSCLES, bool Vegan,
                                                                        bool Vege)
        {

            //stwożenie listy gdzie UserId == podanemu UserID
            #region TWOZENIE LISTY
            var query = (from objRecipe in _db.Recipes
                         join objRecipeList in _db.RecipeList on
                         objRecipe.Id equals objRecipeList.RecipeId
                         //join objImageList in _db.ImageList on
                         //objRecipe.Id equals objImageList.RecipeId
                         //join objImage in _db.Image on
                         //objImageList.ImageId equals objImage.Id
                         join objAllergens in _db.Allergens on
                         objRecipe.AllergenId equals objAllergens.Id
                         join objTags in _db.Tags on
                         objRecipe.TagId equals objTags.Id
                         select new RecipeForFilterDto
                         {
                             Id = objRecipeList.RecipeId,
                             UserId = objRecipe.UserId,
                             RecipeName = objRecipe.RecipeName,
                             Date = objRecipe.Date,
                             Rating = objRecipeList.Rating,
                             Photo = objRecipe.Photo,
                             //Photo = objImage.ImageName,
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
                         }).Where(x => x.UserId == id).ToList();
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
            //query=Filter(filtersDto, query);
            #endregion

            //zgupowanie po id
            var group = query.GroupBy(x => x.Id);

            //twożenie zmiennych
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
                        if (item2.Rating != 0)
                            counter++;
                        ratingSum += item2.Rating;
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
        

    }
}
