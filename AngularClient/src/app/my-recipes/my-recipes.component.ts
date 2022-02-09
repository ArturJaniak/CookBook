import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { RecipeLogedViewClient, RecipePublicListDto } from '../api/ApiClient';
import { MyRecipesService } from '../shared/services/my-recipes.service';

@Component({
  selector: 'app-my-recipes',
  templateUrl: './my-recipes.component.html',
  styleUrls: ['./my-recipes.component.css']
})
export class MyRecipesComponent implements OnInit {

 

  constructor(private recipesService: MyRecipesService, private http: HttpClient, recipeLogedViewClient: RecipeLogedViewClient) { 
    
  }
  recipes : RecipePublicListDto[];
  token: string;
  typesOfShoes: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers'];
  ngOnInit() {
    this.recipesService.getRecipes().subscribe(res=>(this.recipes = res));
    
  }

}


