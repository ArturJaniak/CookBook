import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { RecipeDto, RecipesClient } from '../api/ApiClient';
import { RecipesService } from '../shared/services/recipes.service';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.css']
})
export class RecipeDetailsComponent implements OnInit {

  constructor(private recipesClientService: RecipesService, private recipesClient: RecipesClient, private route: ActivatedRoute, private router: Router) { }
  recipe$ : Observable<any>;
  id:any;
  recipe:any=[];
  ngOnInit(): void {
    //const recipeId = this.route.snapshot.paramMap.get('id');
    //this.recipe = this.recipesClient.recipes_GetRecipe('3d59d6d8-62f7-4078-b4ac-56e5b42e1184');
    

    // this.route.queryParams.subscribe(params => {
    // this.recipe.id = params['id'];})
    this.recipesClient.recipes_GetRecipe('3d59d6d8-62f7-4078-b4ac-56e5b42e1184').subscribe(res=>(this.recipe = res));
    }
}