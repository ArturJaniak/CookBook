import { Component, OnInit } from '@angular/core';
import { RecipeDto } from '../api/ApiClient';
import { RecipesService } from '../shared/services/recipes.service';

@Component({
  selector: 'app-random-recipe',
  templateUrl: './random-recipe.component.html',
  styleUrls: ['./random-recipe.component.css']
})
export class RandomRecipeComponent implements OnInit {

  constructor(private recipesClientService: RecipesService) { }
  recipe: RecipeDto;

  ngOnInit() {
    this.recipesClientService.getRandomRecipes().subscribe(res => (this.recipe = res));
    // this.recipesClientService.
  }
}
