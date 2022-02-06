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

                                   Ingredients = _db.Ingredients.Select(model => new IndigrentsForRecipeDto()//do poprawy żeby wypisywało tylko własne składniki
                                   {
                                       RecipeId = model.RecipeId,
                                       Ingredient = model.Ingredient
                                   }).ToList()
                               }
                               ).ToList();
            
            RecipeDto recipe = listaRecept.First(model=>model.Id==id);
           
            return recipe ;
            
        }
        //------------------------------------------------------------------------------------------------------------------------------------

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
            myRecipeList.Rating = 0;
            myRecipeList.ifInList = true;
            myRecipeList.RecipeId = newID;
            myRecipeList.UserId = createRecipeDto.Id;
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
        //------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost("delete")]
        public ActionResult DeleteConfirmed(DeleteRecipeDto deleteRecipeDto)
        {
            var id = deleteRecipeDto.RecipId;

            var ingredients = _db.Ingredients.Where(x => x.RecipeId == id).ToList();
            foreach (var ingredient in ingredients)
            {
                _db.Ingredients.Remove(ingredient);
            }
            //------------------------------------------
            //ImageList imageList = _db.ImageList.Single(model => model.RecipeId==id);//dorobić usuwanie listy
            //Image image = _db.Image.Find(imageList.ImageId);
            //_db.Image.Remove(image);

            var imageList3 = _db.ImageList.Where(model => model.RecipeId == id).ToList();//stworzenie listy gdzie występuje id recepty
            foreach (var image2 in imageList3) //dla karzdego elem w liście 
            {
                Image i = _db.Image.Find(image2.ImageId);//znajdzi pojedyncze zdjęcie

                _db.Image.Remove(i); // usuń zdjęcie
                _db.ImageList.Remove(image2);// usuń przypisanie do listy
            }
            //------------------------------------------
            //ImageList imageList2 = _db.ImageList.Find(imageList.Id);
            //_db.ImageList.Remove(imageList2);
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

            var x = updateRecipe.Ingredients2.Count;
            if (file != null)
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
                    //przypisanie wartości do bazy
                    //RecipeDto recipe = listaRecept.Single(model => model.Id == id);

                    //twożenie pojedyńczego zdjęcia w bazie 
                    myImage.Id = Guid.NewGuid();
                    myImage.ImageName = uniqueFileName;

                    //dopisanie zdjęcia do listy
                    //znalezienie istniejącego zdjęcia i przypisanie na jego miejsce nowego
                   
                    myImageList.ImageId = myImage.Id;
                    myImageList.Id= Guid.NewGuid();
                    myImageList.RecipeId = updateRecipe.Id;
                    _db.ImageList.Add(myImageList);

                    


                    _db.Image.Add(myImage);
                    _db.SaveChanges();

                }

            }
            Recipes recipe = _db.Recipes.Find(updateRecipe.Id);
            recipe.Instruction = updateRecipe.Instruction;
            recipe.RecipeName = updateRecipe.RecipeName;
            recipe.IfPublic = updateRecipe.IfPublic;
            recipe.Date = DateTime.Now;
                _db.Recipes.Update(recipe);
            

            //pętla aktualizacji istniejących tabel
            var ingredientList = _db.Ingredients.Where(model => model.RecipeId == updateRecipe.Id).ToList(); // dorobic pętle
            foreach (var ingredient in ingredientList)
            {
                _db.Ingredients.Remove(ingredient);
                _db.SaveChanges();

            }

            for (int i = 0; i < updateRecipe.Ingredients2.Count; i++)
            {
                var myIndigrent = new Ingredients();

                myIndigrent.Id = Guid.NewGuid();
                myIndigrent.RecipeId = updateRecipe.Id;
                myIndigrent.Ingredient = updateRecipe.Ingredients2[i].Ingredient;
                _db.Ingredients.Add(myIndigrent);
                _db.SaveChanges();
            }
            //foreach (var ingredient in updateRecipe.Ingredients2)
            //{
            //    var myIndigrent = new Ingredients();
            //    myIndigrent.Id = Guid.NewGuid();
            //    myIndigrent.RecipeId = updateRecipe.Id;
            //    myIndigrent.Ingredient = ingredient.Ingredient;
            //    _db.Ingredients.Add(myIndigrent);
            //    _db.SaveChanges();

            //}
            //for (int i = 0; i < updateRecipe.Ingredients2.Count; i++)//Baza.count do sprawdzenia
            //{
            //    var ingredientBaza = Baza[i];//pojedynczy id z bazy
            //    var daneNieZBazy = updateRecipe.Ingredients2[i]; // pojedynczy składnik z listy
            //    ingredientBaza.Ingredient = daneNieZBazy.Ingredient;
            //    _db.Ingredients.Update(Baza[i]);
            //    //dorobić zmniejszenie usunięcie składnika
            //    // dodać stwożenie extra id bazy gdy wyjdzie poza zasięg
            //}

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

            Tags tags = _db.Tags.Single(model => model.Id == recipe.TagId); 
            tags.Vege = updateRecipe.Vege;
            tags.Vegan = updateRecipe.Vegan;
            _db.Tags.Update(tags);



            _db.SaveChanges();

            return Ok();
        }

        //------------------------------------------------------------------------------------------------------------------------------------
        [HttpPost("rate")]
        public ActionResult RateRecipe(RateRecipeDto rateRecipe)
        {
            //RecipeList recipeList = _db.RecipeList.Single(model => model.UserId == rateRecipe.UserId && model.RecipeId == rateRecipe.RecipeId);

            ////tylko w sytuacji gdy nie ma wystawionej oceny
            //if (recipeList == null)
            //{
                
            //}
            //else
            //{
            //    recipeList.RecipeId = rateRecipe.RecipeId;
            //    _db.RecipeList.Update(recipeList);

            //}

            var myRecipeList = new RecipeList();
            myRecipeList.Id = Guid.NewGuid();
            myRecipeList.Rating = rateRecipe.Rating;
            myRecipeList.ifInList = false;
            myRecipeList.RecipeId = rateRecipe.RecipeId;
            myRecipeList.UserId = rateRecipe.UserId;
            _db.RecipeList.Add(myRecipeList);
            _db.SaveChanges();
            return Ok();
        }
        //------------------------------------------------------------------------------------------------------------------------------------

        [HttpPost("addToMyList")]
        public ActionResult AddToMyList(Guid recipeId, string userId)
        {

            RecipeList recipeList = _db.RecipeList.Single(model=>model.UserId==userId&&model.RecipeId==recipeId);

            
            if (recipeList==null)
            {
                var myRecipeList = new RecipeList();
                myRecipeList.Id = Guid.NewGuid();
                myRecipeList.Rating = 0;
                myRecipeList.ifInList = true;
                myRecipeList.RecipeId = recipeId;
                myRecipeList.UserId = userId;
                _db.RecipeList.Add(myRecipeList);
                _db.SaveChanges();
            }
            else
            {
                recipeList.ifInList = true;
                _db.RecipeList.Update(recipeList);
                _db.SaveChanges();
            }

            //------------------------------------------------
            return Ok();
        }
        //------------------------------------------------------------------------------------------------------------------------------------

    }
}
