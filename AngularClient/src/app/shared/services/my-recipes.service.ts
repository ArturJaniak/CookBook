import { Injectable } from "@angular/core";
import { RecipeLogedViewClient } from "src/app/api/ApiClient";

@Injectable({
    providedIn: 'root'
  })
  export class MyRecipesService {

      constructor(private recipeLogedViewClient: RecipeLogedViewClient){}
      
      getRecipes(){ 
          return this.recipeLogedViewClient.recipeLogedView_GetMyList(localStorage.getItem("token"));
      }
  }