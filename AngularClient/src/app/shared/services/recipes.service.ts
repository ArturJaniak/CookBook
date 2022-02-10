import { Injectable } from "@angular/core";
import {  RecipesClient } from "src/app/api/ApiClient";

@Injectable({
    providedIn: 'root'
  })
  export class RecipesService {

      constructor(private recipesClientService: RecipesClient){}
      
      getRandomRecipes(){ 
          return this.recipesClientService.getRandomRecipe();
      }
      getRecipes(){
          return this.recipesClientService.getRecipes();
      }
  }