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
        [HttpGet("GetMyList")]// pobranie wszystkich recept
        public ActionResult<IEnumerable<RecipePublicListDto>> GetMyList(string token)
        {
            if (token.Length>0)
            {
                //dekoder tokena
                var stream = token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                //znalezienie elem mail
                var userMail = tokenS.Claims.ElementAt(0).Value;
                //wyszukanie odpowiedniego usera
                var user = _db.Users.Single(model => model.UserName == userMail);

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
                                 Photo = objImage.ImageName,
                                 UserId = objRecipe.UserId
                             }).Where(x=>x.UserId==user.Id).ToList();
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

                return recipePublicListDto;//listaRecept;
            }
            return null;
        }
        //------------------------------------------------------------------------------------------------------------------------------------

        [HttpGet("GetSomeoneList")]// pobranie wszystkich recept
        public ActionResult<IEnumerable<RecipePublicListDto>> GetSomeoneList(string id)
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
                                 Photo = objImage.ImageName,
                                 UserId = objRecipe.UserId
                             }).Where(x => x.UserId == id).ToList();
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

                return recipePublicListDto;//listaRecept;
            }
        

    }
}
