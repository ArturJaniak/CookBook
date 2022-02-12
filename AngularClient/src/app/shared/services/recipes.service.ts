import { Injectable } from "@angular/core";
import {  RecipesClient } from "src/app/api/ApiClient";

@Injectable({
    providedIn: 'root'
  })
  export class RecipesService {

      constructor(private recipesClientService: RecipesClient){}
      
      id:any

      recipeDetails(id){
        return this.recipesClientService.recipes_GetRecipe(id);
      }
      getRandomRecipes(){ 
          return this.recipesClientService.recipes_GetRandomRecipe();
      }
      getRecipes( gluten: boolean,
         shellfish: boolean,
        // eggs: boolean,
        // fish: boolean,
        // peanuts: boolean,
        // soy: boolean,
        // lactose: boolean,
        // celery: boolean,
        // mustard: boolean,
        // sesame: boolean,
        // sulphur_dioxide: boolean,
        // lupine: boolean,
        // muscles: boolean
        ){
          return this.recipesClientService.recipes_GetRecipes( 
            gluten,shellfish 
            // shellfish, eggs,fish, peanuts,
            // soy, lactose, celery, mustard, sesame,
            // sulphur_dioxide, lupine,muscles
            );
      }


  }
