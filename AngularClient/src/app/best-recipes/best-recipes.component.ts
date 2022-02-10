import { Component, OnInit } from '@angular/core';
import { RecipePublicListDto } from '../api/ApiClient';
import { RecipesService } from '../shared/services/recipes.service';

@Component({
  selector: 'app-best-recipes',
  templateUrl: './best-recipes.component.html',
  styleUrls: ['./best-recipes.component.css']
})
export class BestRecipesComponent implements OnInit {

  constructor(private recipesClientService: RecipesService) { }
  recipes : RecipePublicListDto[];

  ngOnInit() {
    this.recipesClientService.getRecipes().subscribe(res=>(this.recipes = res));
  }
}

