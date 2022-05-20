import { Injectable } from "@angular/core";
import { RecipeLogedViewClient, RecipesClient, RecipesLogedClient } from "src/app/api/ApiClient";

@Injectable({
    providedIn: 'root'
})
export class MyRecipesService {

    constructor(private recipeLogedViewClient: RecipeLogedViewClient) { }

    getRecipes() {
        return this.recipeLogedViewClient.recipeLogedView_GetMyList(localStorage.getItem("token"));
    }
    someonesRecipe(user_id: any) {
        return this.recipeLogedViewClient.recipeLogedView_GetSomeoneList(user_id);
    }
}