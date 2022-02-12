import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { FileParameter, RecipesLogedClient, } from 'src/app/api/ApiClient';
import { SharingService } from 'src/app/shared/sharing.service';

@Component({
  selector: 'app-edit-recipe-details',
  templateUrl: './edit-recipe-details.component.html',
  styleUrls: ['./edit-recipe-details.component.css']
})
export class EditRecipeDetailsComponent implements OnInit {

  constructor(private sharingService: SharingService,
     private recipesLogged : RecipesLogedClient) { }
  id:any;
  editableRecipe:any=[];
  recipeName: string;
  instructions: string;
  gluten: boolean;
  shellfish: boolean;
  eggs: boolean;
  fish: boolean;
  peanuts: boolean;
  soy: boolean;
  lactose: boolean;
  celery: boolean;
  mustard: boolean;
  sesame: boolean;
  sulphur_dioxide: boolean;
  lupine: boolean;
  muscles: boolean;
  vegan: boolean;
  vege: boolean;
  file: FileParameter[];
  files: any;
  recipe:any;
  
  ngOnInit(): void {
    this.id =  this.sharingService.getData();
    //this.route.params.subscribe(params => {
    //this.getThatRecipe();
  }
  //getThatRecipe(){
    //this.recipesClient.recipes_GetRecipe(this.id).subscribe(res=>(this.recipe = res));
  //}
  editRecipe(recipeName,instruction,ifPublic,gluten,shellfish,eggs,fish,peanuts,soy,lactose,
    celery,mustard,sesame,sulphur_dioxide,lupine,muscles,vegan,vege){
  
    this.recipesLogged.recipesLoged_UpdateData(localStorage.getItem("token"),this.id, recipeName, instruction,ifPublic,null,gluten,
    shellfish,eggs,fish,peanuts,soy,lactose,
    celery,mustard,sesame,sulphur_dioxide,lupine,muscles,vegan,vege).subscribe(res=>(this.recipe = res))

}
}













  // this.recipe.id = this.id;
    // const token=localStorage.getItem("token");
    // this.recipe.token =token;
    // this.recipe.recipeName = recipeName;
    // this.recipe.instruction = instruction;
    // this.recipe.gluten = gluten;
    // this.recipe.shellfish = shellfish;
    // this.recipe.eggs = eggs;
    // this.recipe.fish = fish;
    // this.recipe.peanuts = peanuts;
    // this.recipe.soy = soy;
    // this.recipe.lactose = lactose;
    // this.recipe.celery = celery;
    // this.recipe.mustard = mustard;
    // this.recipe.sesame = sesame;
    // this.recipe.sulphuR_DIOXIDE = sulphur_dioxide;
    // this.recipe.lupine = lupine;
    // this.recipe.muscles = muscles;
    // this.recipe.vegan = vegan;
    // this.recipe.vege = vege;
    // this.file = file;