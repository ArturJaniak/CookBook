import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Route, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RecipeLogedViewClient, RecipesClient, RecipesLogedClient } from '../api/ApiClient';
import { RecipesService } from '../shared/services/recipes.service';
import { SharingService } from '../shared/sharing.service';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.css']
})
export class RecipeDetailsComponent implements OnInit {

  constructor(
    private recipesClient: RecipesClient,
    private sharingService: SharingService,
    private recipesLogged: RecipesLogedClient,
    private router: Router,
    private recipeLoggedViewClient: RecipeLogedViewClient
  ) { }
  recipe$: Observable<any>;
  id: any;
  recipe: any = [];
  token = localStorage.getItem("token");
  recipe_id: any;
  user_id: any;
  rateValue: number;
  rating: number;
  rate: any
  newRecipe: any = [];
  ngOnInit(): void {
    this.id = this.sharingService.getData();
    this.getThatRecipe();
  }
  rateRecipe(rateValue) {
    this.rateValue = rateValue;

    this.recipesLogged.recipesLoged_RateRecipe(this.token, this.id, rateValue).subscribe(res => (this.recipe.rating) = res);

  }
  getThatRecipe() {
    this.recipesClient.recipes_GetRecipe(this.id, this.token).subscribe(res => (this.recipe = res));
  }
  editRecipeDetail(recipe_id: any) {

    let url: string = "/editDetailsRecipe/" + this.id;
    this.router.navigateByUrl(url);
    this.sharingService.setData(this.id);
  }
  addToMyList() {
    this.recipesLogged.recipesLoged_AddToMyList(this.id, this.token).subscribe(res => (this.recipe = res));

  }
  viewPersonList(user_id: any) {
    let url: string = "/someonesRecipe/" + user_id
    this.router.navigateByUrl(url);
    this.sharingService.setData(user_id);
    this.recipeLoggedViewClient.recipeLogedView_GetSomeoneList(this.id)
  }


}