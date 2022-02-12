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
        }

        [HttpPost("Add")]
        public ActionResult CreateRecipe(string token)
        {
            if (token != null)
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

                var newID = Guid.NewGuid();
                //------------------------------------------
                var myAllergens = new Allergens();
                #region STWOENIE ALERGENÓW
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
                #endregion
                //------------------------------------------
                var myTags = new Tags();
                #region STWOZENIE TAGÓW
                myTags.Id = newID;
                myTags.Vege = false;
                myTags.Vegan = false;
                #endregion
                //------------------------------------------
                var myRecipe = new Recipes();
                #region STWOENIE RECEPTY
                myRecipe.Id = newID;
                myRecipe.RecipeName = "New Recipe";
                //myRecipe.Photo = null;          
                myRecipe.Instruction = "Your Instruction";
                myRecipe.IfPublic = false;
                myRecipe.Date = DateTime.Now;
                myRecipe.AllergenId = newID;
                myRecipe.TagId = newID;
                myRecipe.UserId = user.Id;
                #endregion
                //------------------------------------------
                var myPhoto = new Image();
                #region PRZYPISANIE ZDJĘCIA
                Guid photoId = Guid.NewGuid();
                myPhoto.Id = photoId;
                myPhoto.ImageName = "new.png";
                #endregion
                //------------------------------------------
                var myPhotoList = new ImageList();
                #region PRZYPISANIE ZDJĘCIA I RECEPTY DO LISTY ZDJĘĆ
                myPhotoList.Id = Guid.NewGuid();
                myPhotoList.ImageId = photoId;
                myPhotoList.RecipeId = newID;
                #endregion
                //------------------------------------------
                var myRecipeList = new RecipeList();
                #region PRZYPISANIE USERA I RECEPTY DO LISTY RECEPT
                myRecipeList.Id = Guid.NewGuid();
                myRecipeList.Rating = 0;
                myRecipeList.ifInList = true;
                myRecipeList.RecipeId = newID;
                myRecipeList.UserId = user.Id;
                #endregion
                //------------------------------------------
                var myIngredientsList = new Ingredients();
                #region STWOZENIE SKŁADNIKA
                myIngredientsList.Id = Guid.NewGuid();
                myIngredientsList.Ingredient = "New Ingredient";
                myIngredientsList.RecipeId = newID;
                #endregion

                //dodanie stwożonych ele do bazy
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
            if (deleteRecipeDto.token != null)//czy user jest zalogowany
            {
                //przypisanie podanego id do zmiennej
                var id = deleteRecipeDto.RecipId;

                //dekoder tokena
                #region DEKODOWANIE TOKENA
                var stream = deleteRecipeDto.token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                var x = tokenS.Claims.ElementAt(0).Value;
                #endregion
                //znalezienie usera na podstawie emaila z dekodera
                var userTokenID = _db.Users.Single(model => model.UserName == x);
                Recipes recipe2 = _db.Recipes.Find(id);

                //sprawdzenie czy user jest właścicielem recepty
                if (recipe2.UserId == userTokenID.Id)
                {
                    //stwożenie listy składników które należą do recepty
                    var ingredients = _db.Ingredients.Where(x => x.RecipeId == id).ToList();
                    foreach (var ingredient in ingredients)
                    {
                        //usunięcie składników z bazy
                        _db.Ingredients.Remove(ingredient);
                    }
                    //------------------------------------------

                    //ścieżka do zdjęć
                    string fullPath = Path.GetFullPath(@"Imagines");
                    var imageList3 = _db.ImageList.Where(model => model.RecipeId == id).ToList();//stworzenie listy gdzie występuje id recepty
                    foreach (var image2 in imageList3) //dla karzdego elem w liście 
                    {
                        Image i = _db.Image.Find(image2.ImageId);//znajdzi pojedyncze zdjęcie

                        _db.Image.Remove(i); // usuń zdjęcie
                        _db.ImageList.Remove(image2);// usuń przypisanie do listy
                    }
                    //------------------------------------------
                    //usunięcie Recipe list wszędzie tam gdzie występuję recepta
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

        [HttpPost("updateData")]
        public ActionResult UpdateData(UpdateRecipe updateRecipe)
        {

            if (updateRecipe.token != null)
            {
                //dekoder tokena
                #region DEKODER TOKENA
                var stream = updateRecipe.token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                #endregion
                //znalezienie elem mail
                var userMail = tokenS.Claims.ElementAt(0).Value;
                //wyszukanie odpowiedniego usera
                var user = _db.Users.Single(model => model.UserName == userMail);
                Recipes r = _db.Recipes.Find(updateRecipe.Id);//znajdzi pojedyncze zdjęcie
                //sprawdzenie czy user jest właścicielem recepty 
                if (user.Id == r.UserId)
                {

                    //var x = updateRecipe.Ingredients2.Count;
                    
                    //przypisanie starych/nowych danych do recepty
                    #region AKTUALIZACJA DANYCH W RECEPCIE
                    Recipes recipe = _db.Recipes.Find(updateRecipe.Id);
                    recipe.Instruction = updateRecipe.Instruction;
                    recipe.RecipeName = updateRecipe.RecipeName;
                    recipe.IfPublic = updateRecipe.IfPublic;
                    recipe.Date = DateTime.Now;
                    _db.Recipes.Update(recipe);
                    #endregion

                    //usunięcie indigrentów
                    #region USUNIĘCIE SKŁADNIKÓW
                    var ingredientList = _db.Ingredients.Where(model => model.RecipeId == updateRecipe.Id).ToList(); // dorobic pętle
                    foreach (var ingredient in ingredientList)
                    {
                        _db.Ingredients.Remove(ingredient);
                        _db.SaveChanges();

                    }
                    #endregion

                    //stwożenie na nowo indigrentów
                    if (updateRecipe.Ingredients2 !=null)
                    {
                        #region STWOENIE SKŁADNIKÓW
                        for (int i = 0; i < updateRecipe.Ingredients2.Count; i++)
                        {
                            var myIndigrent = new Ingredients();

                            myIndigrent.Id = Guid.NewGuid();
                            myIndigrent.RecipeId = updateRecipe.Id;
                            myIndigrent.Ingredient = updateRecipe.Ingredients2[i].Ingredient;
                            _db.Ingredients.Add(myIndigrent);
                            _db.SaveChanges();
                        }
                        #endregion
                    }


                    //aktualizacja alergenów
                    #region AKTUALIZACJA ALERGENÓW
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
                    #endregion

                    //aktualizacja Tagów
                    #region AKTUALIZACJA TAGÓW
                    Tags tags = _db.Tags.Single(model => model.Id == recipe.TagId);
                    tags.Vege = updateRecipe.Vege;
                    tags.Vegan = updateRecipe.Vegan;
                    _db.Tags.Update(tags);
                    #endregion


                    _db.SaveChanges();

                    return Ok();
                }
                return BadRequest();


            }
            return BadRequest();

        }




        [HttpPost("UpdatePhoto")]
        public ActionResult UpdatePhoto(
             IFormFile file , string token, Guid id)
        {

            if (token != null)
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
                Recipes r = _db.Recipes.Find(id);//znajdzi pojedyncze zdjęcie
                //sprawdzenie czy user jest właścicielem recepty 
                if (user.Id == r.UserId)
                {

                    
                    if (file != null)
                    {
                        //usunięcie i tabeli lista zdjęć i tabeli zdjęć gdzie idRecepty == id Recepty
                        var imageList = _db.ImageList.Where(model => model.RecipeId == id).ToList();
                        #region USUNIĘCIE ZDJĘĆ
                        foreach (var item in imageList)
                        {

                            Image img = _db.Image.Find(item.ImageId);
                           
                                _db.Image.Remove(img);

                            
                            _db.ImageList.Remove(item);
                            //_db.SaveChanges();

                        }
                        #endregion

                        string uniqueFileName = null;
                        var myImage = new Image();
                        var myImageList = new ImageList();
                        //stwożenie na nowo zdjęć 
                        #region STOWENIE ZDJĘĆ
                        
                            //pobranie pełnej ścieżki

                            //string fullPath = Path.GetFullPath(@"GitHub\CookBook\AngularClient\src\assets");//     CookBook\AngularClient\src\assets
                            string path = @"AngularClient\src\assets";
                            string newPath = Path.GetFullPath(Path.Combine(@"..\..\..\", path));
                            //------------------------------------------
                            //przypisanie unikalnej nazwy
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName.ToString();
                            //------------------------------------------
                            //stwożenie pełnej ścieżki do zdjęcia
                            string filePath = Path.Combine(newPath, uniqueFileName);//@"C:\Users\Tomek\Documents\GitHub\CookBook\AngularClient\src\assets"
                            //------------------------------------------
                            //stwożenie zdjęcia w danym fold
                            file.CopyTo(new FileStream(filePath, FileMode.Create));
                            //------------------------------------------             

                            //twożenie pojedyńczego zdjęcia w bazie 
                            myImage.Id = Guid.NewGuid();
                            myImage.ImageName = uniqueFileName;

                            //dopisanie zdjęcia do listy
                            //znalezienie istniejącego zdjęcia i przypisanie na jego miejsce nowego                  
                            myImageList.ImageId = myImage.Id;
                            myImageList.Id = Guid.NewGuid();
                            myImageList.RecipeId = id;
                            _db.ImageList.Add(myImageList);

                            _db.Image.Add(myImage);
                            //_db.SaveChanges();
                        
                        #endregion
                    }
                   

                    _db.SaveChanges();

                    return Ok();
                }
                return BadRequest();


            }
            return BadRequest();
        }



        [HttpPost("upload")]
        public ActionResult UploadMultiples(
         //   [ModelBinder(BinderType = typeof(JsonModelBinder))]
             UpdateRecipe updateRecipe, IList<IFormFile> file)
        {

            if (updateRecipe.token!=null)
            {
                //dekoder tokena
                #region DEKODER TOKENA
                var stream = updateRecipe.token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                #endregion
                //znalezienie elem mail
                var userMail = tokenS.Claims.ElementAt(0).Value;
                //wyszukanie odpowiedniego usera
                var user = _db.Users.Single(model => model.UserName == userMail);
                Recipes r = _db.Recipes.Find(updateRecipe.Id);//znajdzi pojedyncze zdjęcie
                //sprawdzenie czy user jest właścicielem recepty 
                if (user.Id == r.UserId)
                {

                    var x = updateRecipe.Ingredients2.Count;
                    if (file.Count > 0)
                    {
                        //usunięcie i tabeli lista zdjęć i tabeli zdjęć gdzie idRecepty == id Recepty
                        var imageList = _db.ImageList.Where(model => model.RecipeId == updateRecipe.Id).ToList();
                        #region USUNIĘCIE ZDJĘĆ
                        foreach (var item in imageList)
                        {

                            Image img = _db.Image.Find(item.ImageId);
                            if (img.ImageName != "new.png")
                            {
                                _db.Image.Remove(img);

                            }
                            _db.ImageList.Remove(item);
                            //_db.SaveChanges();

                        }
                        #endregion

                        string uniqueFileName = null;
                        var myImage = new Image();
                        var myImageList = new ImageList();
                        //stwożenie na nowo zdjęć 
                        #region STOWENIE ZDJĘĆ
                        foreach (var image in file)
                        {
                            //pobranie pełnej ścieżki
                           
                            //string fullPath = Path.GetFullPath(@"GitHub\CookBook\AngularClient\src\assets");//     CookBook\AngularClient\src\assets
                            string path = @"AngularClient\src\assets";
                            string newPath = Path.GetFullPath(Path.Combine( @"..\..\..\", path));
                            //------------------------------------------
                            //przypisanie unikalnej nazwy
                            uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName.ToString();
                            //------------------------------------------
                            //stwożenie pełnej ścieżki do zdjęcia
                            string filePath = Path.Combine(newPath, uniqueFileName);//@"C:\Users\Tomek\Documents\GitHub\CookBook\AngularClient\src\assets"
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
                            //_db.SaveChanges();
                        }
                        #endregion
                    }
                    //przypisanie starych/nowych danych do recepty
                    #region AKTUALIZACJA DANYCH W RECEPCIE
                    Recipes recipe = _db.Recipes.Find(updateRecipe.Id);
                    recipe.Instruction = updateRecipe.Instruction;
                    recipe.RecipeName = updateRecipe.RecipeName;
                    recipe.IfPublic = updateRecipe.IfPublic;
                    recipe.Date = DateTime.Now;
                    _db.Recipes.Update(recipe);
                    #endregion

                    //usunięcie indigrentów
                    #region USUNIĘCIE SKŁADNIKÓW
                    var ingredientList = _db.Ingredients.Where(model => model.RecipeId == updateRecipe.Id).ToList(); // dorobic pętle
                    foreach (var ingredient in ingredientList)
                    {
                        _db.Ingredients.Remove(ingredient);
                        _db.SaveChanges();

                    }
                    #endregion

                    //stwożenie na nowo indigrentów
                    #region STWOENIE SKŁADNIKÓW
                    for (int i = 0; i < updateRecipe.Ingredients2.Count; i++)
                    {
                        var myIndigrent = new Ingredients();

                        myIndigrent.Id = Guid.NewGuid();
                        myIndigrent.RecipeId = updateRecipe.Id;
                        myIndigrent.Ingredient = updateRecipe.Ingredients2[i].Ingredient;
                        _db.Ingredients.Add(myIndigrent);
                        _db.SaveChanges();
                    }
                    #endregion

                    //aktualizacja alergenów
                    #region AKTUALIZACJA ALERGENÓW
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
                    #endregion

                    //aktualizacja Tagów
                    #region AKTUALIZACJA TAGÓW
                    Tags tags = _db.Tags.Single(model => model.Id == recipe.TagId);
                    tags.Vege = updateRecipe.Vege;
                    tags.Vegan = updateRecipe.Vegan;
                    _db.Tags.Update(tags);
                    #endregion


                    _db.SaveChanges();

                    return Ok();
                }
                return BadRequest();


            }
            return BadRequest();
        }

        //------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("rate")]
        public ActionResult RateRecipe(RateRecipeDto rateRecipe)
        {
            if (rateRecipe.token!=null)
            {
                //dekoder tokena
                #region DEKODER TOKENA
                var stream = rateRecipe.token;
                var handler = new JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = jsonToken as JwtSecurityToken;
                #endregion

                //znalezienie elem mail
                var userMail = tokenS.Claims.ElementAt(0).Value;

                //wyszukanie odpowiedniego usera
                var user = _db.Users.Single(model => model.UserName == userMail);

                //zliczenie ilości list gdzie jest taki sam UserId i RecipeId
                var recipeList2 = _db.RecipeList.Where(model => model.UserId == user.Id && model.RecipeId == rateRecipe.RecipeId).ToList().Count;

                //sprawdzenie czy dana lista/obiekt istnieje                  
                if (recipeList2 == 0)
                {
                    //jeżeli nie to stwóż nowy obiekt w recipe list
                    var myRecipeList = new RecipeList();
                    myRecipeList.Id = Guid.NewGuid();
                    myRecipeList.Rating = rateRecipe.Rating;
                    myRecipeList.ifInList = false;
                    myRecipeList.RecipeId = rateRecipe.RecipeId;
                    myRecipeList.UserId = user.Id;
                    _db.RecipeList.Add(myRecipeList);
                    _db.SaveChanges();
                }
                else
                {
                    //jeżeli tak to aktualizuj recipe list
                    var recipeList = _db.RecipeList.Single(model => model.UserId == user.Id && model.RecipeId == rateRecipe.RecipeId);
                    recipeList.Rating = rateRecipe.Rating;
                    _db.RecipeList.Update(recipeList);
                    _db.SaveChanges();

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

            if (token!=null)
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

                //zliczenie ilości list gdzie jest taki sam UserId i RecipeId
                var recipeList2 = _db.RecipeList.Where(model => model.UserId == user.Id && model.RecipeId == recipeId).ToList().Count;

                //sprawdzenie czy dana lista/obiekt istnieje                  
                if (recipeList2 == 0)
                {
                    //jeżeli nie to stwóż nowy obiekt w recipe list
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
                    //jeżeli tak to aktualizuj recipe list
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
