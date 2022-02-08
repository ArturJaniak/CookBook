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
    public class RecipesLogedController : Controller
    {
        public RepositoryContext _db;

        public RecipesLogedController(RepositoryContext db)
        {
            _db = db;
            //_authResponse=authResponse;
        }

        [HttpPost("Add")]
        public ActionResult CreateRecipe(CreateRecipeDto createRecipeDto)
        {
            if (createRecipeDto.token.Length>0)
            {
                //dekoder tokena
                var stream = createRecipeDto.token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                //znalezienie elem mail
                var userMail = tokenS.Claims.ElementAt(0).Value;
                //wyszukanie odpowiedniego usera
                var user = _db.Users.Single(model => model.UserName == userMail);

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
                myRecipe.UserId = user.Id;
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
                myRecipeList.Rating = 0;
                myRecipeList.ifInList = true;
                myRecipeList.RecipeId = newID;
                myRecipeList.UserId = user.Id;
                //------------------------------------------
                var myIngredientsList = new Ingredients();
                myIngredientsList.Id = Guid.NewGuid();
                myIngredientsList.Ingredient = "New Ingredient";
                myIngredientsList.RecipeId = newID;


                _db.Allergens.Add(myAllergens);
                _db.Tags.Add(myTags);
                _db.Recipes.Add(myRecipe);
                _db.Image.Add(myPhoto);
                _db.ImageList.Add(myPhotoList);
                _db.RecipeList.Add(myRecipeList);
                _db.Ingredients.Add(myIngredientsList);
                _db.SaveChanges();
                return Ok();
            }
           return BadRequest();
        }
        //------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost("delete")]
        public ActionResult DeleteConfirmed(DeleteRecipeDto deleteRecipeDto)
        {
            if (deleteRecipeDto.token.Length>0)//czy user jest zalogowany
            {
                var id = deleteRecipeDto.RecipId;

                //dekoder tokena
                var stream = deleteRecipeDto.token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                var x = tokenS.Claims.ElementAt(0).Value;
                //znalezienie usera na podstawie emaila z dekodera
                var userTokenID = _db.Users.Single(model => model.UserName == x);
                Recipes recipe2 = _db.Recipes.Find(id);

                //sprawdzenie czy user jest właścicielem recepty
                if (recipe2.UserId == userTokenID.Id)
                {

                    var ingredients = _db.Ingredients.Where(x => x.RecipeId == id).ToList();
                    foreach (var ingredient in ingredients)
                    {
                        _db.Ingredients.Remove(ingredient);
                    }
                    //------------------------------------------

                    string fullPath = Path.GetFullPath(@"Imagines");//ścieżka do zdjęć
                    var imageList3 = _db.ImageList.Where(model => model.RecipeId == id).ToList();//stworzenie listy gdzie występuje id recepty
                    foreach (var image2 in imageList3) //dla karzdego elem w liście 
                    {
                        Image i = _db.Image.Find(image2.ImageId);//znajdzi pojedyncze zdjęcie

                        _db.Image.Remove(i); // usuń zdjęcie
                        _db.ImageList.Remove(image2);// usuń przypisanie do listy
                    }
                    //------------------------------------------
                    var recipeList = _db.RecipeList.Where(model => model.RecipeId == id).ToList();
                    foreach (var item in recipeList)
                    {
                        _db.RecipeList.Remove(item);
                    }
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
                return BadRequest();
            }
            return BadRequest();
        }
        //------------------------------------------------------------------------------------------------------------------------------------

        public class JsonModelBinder : IModelBinder
        {
            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                if (bindingContext == null)
                {
                    throw new ArgumentNullException(nameof(bindingContext));
                }

                // Check the value sent in
                var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (valueProviderResult != ValueProviderResult.None)
                {
                    bindingContext.ModelState.SetModelValue(bindingContext.ModelName, valueProviderResult);

                    // Attempt to convert the input value
                    var valueAsString = valueProviderResult.FirstValue;
                    var result = Newtonsoft.Json.JsonConvert.DeserializeObject(valueAsString, bindingContext.ModelType);
                    if (result != null)
                    {
                        bindingContext.Result = ModelBindingResult.Success(result);
                        return Task.CompletedTask;
                    }
                }

                return Task.CompletedTask;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost("upload")]
        public IActionResult UploadMultiples(
            [ModelBinder(BinderType = typeof(JsonModelBinder))]
             UpdateRecipe updateRecipe, IList<IFormFile> file)
        {

            if (updateRecipe.token.Length>0)
            {
                //dekoder tokena
                var stream = updateRecipe.token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                //znalezienie elem mail
                var userMail = tokenS.Claims.ElementAt(0).Value;
                //wyszukanie odpowiedniego usera
                var user = _db.Users.Single(model => model.UserName == userMail);

                if (user.Id==updateRecipe.Id.ToString())
                {
                    var x = updateRecipe.Ingredients2.Count;
                    if (file.Count > 0)
                    {
                        //usunięcie i stwożenie na nowo tabeli lista zdjęć i tabeli zdjęć gdzie idRecepty == id Recepty
                        var imageList = _db.ImageList.Where(model => model.RecipeId == updateRecipe.Id).ToList();
                        foreach (var item in imageList)
                        {

                            Image img = _db.Image.Find(item.ImageId);
                            if (img.ImageName != "new.png")
                            {
                                _db.Image.Remove(img);

                            }
                            _db.ImageList.Remove(item);
                            _db.SaveChanges();

                        }
                        string uniqueFileName = null;
                        var myImage = new Image();
                        var myImageList = new ImageList();

                        foreach (var image in file)
                        {
                            //pobranie pełnej ścieżki
                            string fullPath = Path.GetFullPath(@"Imagines");
                            //------------------------------------------
                            //przypisanie unikalnej nazwy
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName.ToString();
                            //------------------------------------------
                            //stwożenie pełnej ścieżki do zdjęcia
                            string filePath = Path.Combine(fullPath, uniqueFileName);
                            //------------------------------------------
                            //stwożenie zdjęcia w danym fold
                            image.CopyTo(new FileStream(filePath, FileMode.Create));
                            //------------------------------------------             

                            //twożenie pojedyńczego zdjęcia w bazie 
                            myImage.Id = Guid.NewGuid();
                            myImage.ImageName = uniqueFileName;

                            //dopisanie zdjęcia do listy
                            //znalezienie istniejącego zdjęcia i przypisanie na jego miejsce nowego                  
                            myImageList.ImageId = myImage.Id;
                            myImageList.Id = Guid.NewGuid();
                            myImageList.RecipeId = updateRecipe.Id;
                            _db.ImageList.Add(myImageList);

                            _db.Image.Add(myImage);
                            _db.SaveChanges();
                        }
                    }
                    //przypisanie starych/nowych danych do recepty
                    Recipes recipe = _db.Recipes.Find(updateRecipe.Id);
                    recipe.Instruction = updateRecipe.Instruction;
                    recipe.RecipeName = updateRecipe.RecipeName;
                    recipe.IfPublic = updateRecipe.IfPublic;
                    recipe.Date = DateTime.Now;
                    _db.Recipes.Update(recipe);


                    //usunięcie indigrentów
                    var ingredientList = _db.Ingredients.Where(model => model.RecipeId == updateRecipe.Id).ToList(); // dorobic pętle
                    foreach (var ingredient in ingredientList)
                    {
                        _db.Ingredients.Remove(ingredient);
                        _db.SaveChanges();

                    }
                    //stwożenie na nowo indigrentów
                    for (int i = 0; i < updateRecipe.Ingredients2.Count; i++)
                    {
                        var myIndigrent = new Ingredients();

                        myIndigrent.Id = Guid.NewGuid();
                        myIndigrent.RecipeId = updateRecipe.Id;
                        myIndigrent.Ingredient = updateRecipe.Ingredients2[i].Ingredient;
                        _db.Ingredients.Add(myIndigrent);
                        _db.SaveChanges();
                    }

                    //aktualizacja alergenów
                    Allergens allergens = _db.Allergens.Single(model => model.Id == recipe.AllergenId);
                    allergens.FISH = updateRecipe.FISH;
                    allergens.CELERY = updateRecipe.CELERY;
                    allergens.EGGS = updateRecipe.EGGS;
                    allergens.GLUTEN = updateRecipe.GLUTEN;
                    allergens.Lactose = updateRecipe.Lactose;
                    allergens.LUPINE = updateRecipe.LUPINE;
                    allergens.MUSCLES = updateRecipe.MUSCLES;
                    allergens.MUSTARD = updateRecipe.MUSTARD;
                    allergens.PEANUTS = updateRecipe.PEANUTS;
                    allergens.SESAME = updateRecipe.SESAME;
                    allergens.SHELLFISH = updateRecipe.SHELLFISH;
                    allergens.SOY = updateRecipe.SOY;
                    allergens.SULPHUR_DIOXIDE = updateRecipe.SULPHUR_DIOXIDE;
                    _db.Allergens.Update(allergens);
                    //aktualizacja Tagów
                    Tags tags = _db.Tags.Single(model => model.Id == recipe.TagId);
                    tags.Vege = updateRecipe.Vege;
                    tags.Vegan = updateRecipe.Vegan;
                    _db.Tags.Update(tags);



                    _db.SaveChanges();

                    return Ok();
                }

                
            }
           return BadRequest();
        }

        //------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("rate")]
        public ActionResult RateRecipe(RateRecipeDto rateRecipe)
        {
            if (rateRecipe.token.Length>0)
            {
                //dekoder tokena
                var stream = rateRecipe.token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                //znalezienie elem mail
                var userMail = tokenS.Claims.ElementAt(0).Value;
                //wyszukanie odpowiedniego usera
                var user = _db.Users.Single(model => model.UserName == userMail);
                
                var recipeList2 = _db.RecipeList.Where(model => model.UserId == user.Id && model.RecipeId == rateRecipe.RecipeId).ToList().Count;
                
                    
                if (recipeList2 == 0)
                {
                    var myRecipeList = new RecipeList();
                    myRecipeList.Id = Guid.NewGuid();
                    myRecipeList.Rating = rateRecipe.Rating;
                    myRecipeList.ifInList = false;
                    myRecipeList.RecipeId = rateRecipe.RecipeId;
                    myRecipeList.UserId = user.Id;
                    _db.RecipeList.Add(myRecipeList);
                }
                else
                {
                    var recipeList = _db.RecipeList.Single(model => model.UserId == user.Id && model.RecipeId == rateRecipe.RecipeId);
                    recipeList.RecipeId = rateRecipe.RecipeId;
                    _db.RecipeList.Update(recipeList);
                }

                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        //------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost("addToMyList")]
        public ActionResult AddToMyList(Guid recipeId,string token)
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

                var recipeList2 = _db.RecipeList.Where(model => model.UserId == user.Id && model.RecipeId == recipeId).ToList().Count;

                if (recipeList2 == 0)
                {
                    var myRecipeList = new RecipeList();
                    myRecipeList.Id = Guid.NewGuid();
                    myRecipeList.Rating = 0;
                    myRecipeList.ifInList = true;
                    myRecipeList.RecipeId = recipeId;
                    myRecipeList.UserId = user.Id;
                    _db.RecipeList.Add(myRecipeList);
                }
                else
                {
                    var recipeList = _db.RecipeList.Single(model => model.UserId == user.Id && model.RecipeId == recipeId);
                    recipeList.ifInList = true;
                    _db.RecipeList.Update(recipeList);
                }

                _db.SaveChanges();
                return Ok();
            }
            return BadRequest();
        }
        //------------------------------------------------------------------------------------------------------------------------------------
    }
}
