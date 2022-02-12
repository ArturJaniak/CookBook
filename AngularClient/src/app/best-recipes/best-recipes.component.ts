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
  eggs: boolean
  fish: boolean
  peanuts: boolean
  soy: boolean
  lactose: boolean
  celery: boolean
  mustard: boolean
  sesame: boolean
  sulphur_dioxide: boolean
  lupine: boolean
  muscles: boolean
  typesOfShoes: string[] = ['Gluten','Shellfish'];

  ngOnInit() {
    // this.recipesClientService.getRecipes( 
    //   this.gluten = boolean
    //   // this.shellfish,

    //   ).subscribe(res=>(this.recipes = res));
      this.filter(this.gluten,this.shellfish,this.eggs,this.fish,this.peanuts,this.soy,this.lactose,this.celery,this.mustard,this.sesame,this.sulphur_dioxide,this.lupine,this.muscles);
  }
  filter(gluten:boolean, shellfish:boolean,eggs: boolean,
    fish: boolean,
    peanuts: boolean,
    soy: boolean,
    lactose: boolean,
    celery: boolean,
    mustard: boolean,
    sesame: boolean,
    sulphur_dioxide: boolean,
    lupine: boolean,
    muscles: boolean){
    this.recipesClientService.getRecipes(
      this.gluten = gluten,
      this.shellfish = shellfish,
      this.eggs = eggs,
      this.fish = fish,
      this.peanuts = peanuts,
      this.soy = soy,
      this.lactose = lactose,
      this.celery = celery,
      this.mustard = mustard,
      this.sesame = sesame,
      this.sulphur_dioxide = sulphur_dioxide,
      this.lupine = lupine,
      this.muscles = muscles
    ).subscribe(res=>(this.recipes = res))
  }
  viewRecipeDetail(recipe_id : any){
    let url: string = "/detailsRecipe/" + recipe_id
         this.router.navigateByUrl(url);
      }
}

