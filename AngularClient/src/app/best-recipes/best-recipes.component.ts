import { Component, Input, OnInit } from '@angular/core';
import { RecipePublicListDto } from '../api/ApiClient';
import { RecipesService } from '../shared/services/recipes.service';

@Component({
  selector: 'app-best-recipes',
  templateUrl: './best-recipes.component.html',
  styleUrls: ['./best-recipes.component.css']
})
export class BestRecipesComponent implements OnInit {

  constructor(private recipesClientService: RecipesService) { }
  recipes: RecipePublicListDto[]
  gluten: true
  shellfish: false
  eggs: false
  fish: false
  peanuts: false
  soy: false
  lactose: false
  celery: false
  mustard: false
  sesame: false
  sulphur_dioxide: false
  lupine: false
  muscles: false
  typesOfShoes: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers'];

  ngOnInit() {
    this.recipesClientService.getRecipes( 
      this.gluten = true
      // this.shellfish,
      // this.eggs,
      // this.fish,
      // this.peanuts,
      // this.soy,
      // this.lactose,
      // this.celery,
      // this.mustard,
      // this.sesame,
      // this.sulphur_dioxide,
      // this.lupine,
      // this.muscles
      ).subscribe(res=>(this.recipes = res));
  }
}

