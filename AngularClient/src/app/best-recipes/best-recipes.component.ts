import { Component, Input, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { RecipePublicListDto } from '../api/ApiClient';
import { RecipesService } from '../shared/services/recipes.service';

@Component({
  selector: 'app-best-recipes',
  templateUrl: './best-recipes.component.html',
  styleUrls: ['./best-recipes.component.css']
})
export class BestRecipesComponent implements OnInit {

  constructor(private recipesClientService: RecipesService, private router: Router) { }
  recipes: RecipePublicListDto[]
  gluten: boolean
  shellfish: boolean
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
  typesOfShoes: string[] = ['Gluten','Shellfish'];

  ngOnInit() {
    // this.recipesClientService.getRecipes( 
    //   this.gluten = false
    //   // this.shellfish,
    //   // this.eggs,
    //   // this.fish,
    //   // this.peanuts,
    //   // this.soy,
    //   // this.lactose,
    //   // this.celery,
    //   // this.mustard,
    //   // this.sesame,
    //   // this.sulphur_dioxide,
    //   // this.lupine,
    //   // this.muscles
    //   ).subscribe(res=>(this.recipes = res));
      this.filter(this.gluten,this.shellfish);
  }
  filter(gluten:boolean, shellfish:boolean){
    this.recipesClientService.getRecipes(
      this.gluten = gluten,
      this.shellfish = shellfish
    ).subscribe(res=>(this.recipes = res))
  }
  viewRecipeDetail(recipe_id : any){
    let url: string = "/detailsRecipe/" + recipe_id
         this.router.navigateByUrl(url);
      }
}

