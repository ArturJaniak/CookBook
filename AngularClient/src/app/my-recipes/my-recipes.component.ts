import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { RecipePublicListDto, RecipesLogedClient } from '../api/ApiClient';
import { MyRecipesService } from '../shared/services/my-recipes.service';
import { RecipesService } from '../shared/services/recipes.service';
import { SharingService } from '../shared/sharing.service';

@Component({
  selector: 'app-my-recipes',
  templateUrl: './my-recipes.component.html',
  styleUrls: ['./my-recipes.component.css']
})
export class MyRecipesComponent implements OnInit {


  constructor(private router: Router,
    private recipesService: MyRecipesService,
    private recipeLogged: RecipesLogedClient,
    private route: ActivatedRoute,
    private sharingService: SharingService,
    private recipesClientService: RecipesService) { }
  recipes$: Observable<any>;
  selectedId: any;
  recipes: RecipePublicListDto[];
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

  token: string;
  typesOfShoes: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers'];
  respones: any;

  ngOnInit() {
    this.recipes$ = this.route.paramMap.pipe(switchMap(params => {
      this.selectedId = Number(params.get('id'));
      return this.recipesService.getRecipes();
    }))
    this.filter(this.gluten, this.shellfish, this.eggs, this.fish, this.peanuts, this.soy, this.lactose, this.celery, this.mustard, this.sesame, this.sulphur_dioxide, this.lupine, this.muscles);

    this.recipesService.getRecipes().subscribe(res => (this.recipes = res));

  }
  filter(gluten: boolean, shellfish: boolean, eggs: boolean,
    fish: boolean,
    peanuts: boolean,
    soy: boolean,
    lactose: boolean,
    celery: boolean,
    mustard: boolean,
    sesame: boolean,
    sulphur_dioxide: boolean,
    lupine: boolean,
    muscles: boolean) {
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
    ).subscribe(res => (this.recipes = res))
  }
  viewRecipeDetail(recipe_id: any) {
    let url: string = "/detailsRecipe/" + recipe_id
    this.router.navigateByUrl(url);
    //console.log(recipe_id);
    this.sharingService.setData(recipe_id);
  }
  createRecipe() {
    alert("dziala");
    this.recipeLogged.recipesLoged_CreateRecipe(localStorage.getItem("token")).subscribe(res => (this.respones = res));
  }
  deleteRecipe(id: any) {
    this.recipeLogged.recipesLoged_DeleteConfirmed(id, localStorage.getItem("token")).subscribe(res => (this.respones = res));
  }
  // createRecipe(){
  //   this.recipesService.createRecipe();
  //   //alert("dziala");

  // }

}


