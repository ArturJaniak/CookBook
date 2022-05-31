import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RecipesLogedClient, RecipeLogedViewClient, RecipeDto, RecipesClient } from '../api/ApiClient';
import { RecipesService } from '../shared/services/recipes.service';
import { SharingService } from '../shared/sharing.service';

@Component({
  selector: 'app-random-recipe',
  templateUrl: './random-recipe.component.html',
  styleUrls: ['./random-recipe.component.css']
})
export class RandomRecipeComponent implements OnInit {

  constructor(private recipesClientService: RecipesService,
    private sharingService: SharingService,
    private recipesLogged: RecipesLogedClient,
    private router: Router,
    private recipeLoggedViewClient: RecipeLogedViewClient,
    private recipesClient: RecipesClient,) { }
  //recipe: RecipeDto;
  recipe$: Observable<any>;
  id: any;
  recipe: any = [];
  token = localStorage.getItem("token");
  recipe_id: any;
  user_id: any;

  ngOnInit() {
    this.recipesClientService.getRandomRecipes().subscribe(res => (this.recipe = res));
    this.id = this.sharingService.getData();
    this.getThatRecipe();

  }
  getThatRecipe() {
    this.recipesClient.recipes_GetRecipe(this.id, this.token).subscribe(res => (this.recipe = res));
  }
  addToMyList(recipe_id: any) {
    this.recipesLogged.recipesLoged_AddToMyList(recipe_id, this.token).subscribe(res => (this.recipe = res));

  }
  viewPersonList(user_id: any) {
    let url: string = "/someonesRecipe/" + user_id
    this.router.navigateByUrl(url);
    this.sharingService.setData(user_id);
    this.recipeLoggedViewClient.recipeLogedView_GetSomeoneList(this.id)
  }
}
