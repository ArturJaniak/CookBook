using Contracts;
using Entities;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly CompanyEmployees.MappingProfile _mappingProfile;
        public RecipesController( RepositoryContext db,
                                  IHostingEnvironment hostingEnvironment)
        {
            
            _db = db;
            this.hostingEnvironment = hostingEnvironment;
        }

        

        [HttpGet]// pobranie wszystkich recept
        public ActionResult<IEnumerable<RecipePublicListDto>> GetRecipes()
        {
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
      
            return query;//listaRecept;
        }
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
                                   RecipeName = objRecipe.RecipeName,
                                   Date = objRecipe.Date,
                                   Rating = objRecipeList.Rating,
                                   Photo = objImage.ImageName
                               }).ToList();
            RecipeDto recipe = listaRecept.Single(model=>model.Id==id);
            return recipe;
            
        }
        [HttpPost]
        public ActionResult CreateRecipe(CreateRecipeDto createRecipeDto)
        {
            var newID = Guid.NewGuid();
            //------------------------------------------
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
            //------------------------------------------
            var myTags = new Tags();
            myTags.Id = newID;

            myTags.Vege = false;
            myTags.Vegan = false;
            //------------------------------------------
            var myRecipe = new Recipes();
            myRecipe.Id = newID;
            myRecipe.RecipeName = "New Recipe";
            //myRecipe.Photo = null;          
            myRecipe.Instruction = "Your Instruction";
            myRecipe.IfPublic = false;
            myRecipe.Date = DateTime.Now;
            myRecipe.AllergenId = newID;
            myRecipe.TagId = newID;
            myRecipe.UserId = createRecipeDto.Id;
            //------------------------------------------
            var myPhoto = new Image();
            Guid photoId = Guid.NewGuid();
            myPhoto.Id = photoId;
            myPhoto.ImageName = "new.png";
            //------------------------------------------
            var myPhotoList = new ImageList();
            myPhotoList.Id = Guid.NewGuid();
            myPhotoList.ImageId = photoId;
            myPhotoList.RecipeId = newID;
            //------------------------------------------
            var myRecipeList = new RecipeList();
            myRecipeList.Id = Guid.NewGuid();
            myRecipeList.Rating = 1;
            myRecipeList.ifInList = true;
            myRecipeList.RecipeId = newID;
            myRecipeList.UserId = createRecipeDto.Id;
            //------------------------------------------
            _db.Allergens.Add(myAllergens);
            _db.Tags.Add(myTags);
            _db.Recipes.Add(myRecipe);
            _db.Image.Add(myPhoto);
            _db.ImageList.Add(myPhotoList);
            _db.RecipeList.Add(myRecipeList);
            _db.SaveChanges();  
            return Ok();
        }

        [HttpPost("delete")]
        public ActionResult DeleteConfirmed(DeleteRecipeDto deleteRecipeDto)
        {
            var id = deleteRecipeDto.RecipId;
            //------------------------------------------
            ImageList imageList = _db.ImageList.Single(model => model.RecipeId==id);
            Image image = _db.Image.Find(imageList.ImageId);
            _db.Image.Remove(image);
            //------------------------------------------
            ImageList imageList2 = _db.ImageList.Find(imageList.Id);
            _db.ImageList.Remove(imageList2);
            //------------------------------------------
            RecipeList recipeList = _db.RecipeList.Single(model => model.RecipeId == id); ;
            _db.RecipeList.Remove(recipeList);
            //------------------------------------------
            Recipes recipe = _db.Recipes.Find(id);
            _db.Recipes.Remove(recipe);
            //------------------------------------------
            Allergens allergens = _db.Allergens.Find(id);
            _db.Allergens.Remove(allergens);
            //------------------------------------------
            Tags tags = _db.Tags.Find(id);
            _db.Tags.Remove(tags);
            //------------------------------------------
            _db.SaveChanges();
            //------------------------------------------
            return Ok();
        }
        [HttpPost("upload")]
        public async Task< IActionResult> UploadMultiples(IFormFile file, UpdateRecipe updateRecipe)
        {

            string uniqueFileName = null;
            if (file != null)
            {
                //pobranie pełnej ścieżki
                string fullPath = Path.GetFullPath(@"Imagines");
                //------------------------------------------
                //przypisanie unikalnej nazwy
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName.ToString();
                //------------------------------------------
                //stwożenie pełnej ścieżki do zdjęcia
                string filePath = Path.Combine(fullPath, uniqueFileName);
                //------------------------------------------
                //stwożenie zdjęcia w danym fold
                file.CopyTo(new FileStream(filePath, FileMode.Create));
                //------------------------------------------             
                //przypisanie wartości do bazy
                var myImage = new Image();
                myImage.Id = Guid.NewGuid();
                myImage.ImageName = uniqueFileName;
                _db.Image.Add(myImage);
                _db.SaveChanges();

            }
            return Ok();
        }


       


    }
}
