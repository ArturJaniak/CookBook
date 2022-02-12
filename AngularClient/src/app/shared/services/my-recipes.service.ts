import { Injectable } from "@angular/core";
import { RecipeLogedViewClient, RecipesClient, RecipesLogedClient } from "src/app/api/ApiClient";

@Injectable({
    providedIn: 'root'
  })
  export class MyRecipesService {

      constructor(private recipeLogedViewClient: RecipeLogedViewClient, private recipeLogged: RecipesLogedClient){}
      
      getRecipes(){ 
          return this.recipeLogedViewClient.recipeLogedView_GetMyList(localStorage.getItem("token"));
      }
  }