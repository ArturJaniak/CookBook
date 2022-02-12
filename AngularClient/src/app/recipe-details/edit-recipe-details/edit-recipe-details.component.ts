import { Component, OnInit } from '@angular/core';
import { RecipeDto, RecipesClient, RecipesLogedClient } from 'src/app/api/ApiClient';
import { SharingService } from 'src/app/shared/sharing.service';

@Component({
  selector: 'app-edit-recipe-details',
  templateUrl: './edit-recipe-details.component.html',
  styleUrls: ['./edit-recipe-details.component.css']
})
export class EditRecipeDetailsComponent implements OnInit {

  constructor(private sharingService: SharingService, private recipesClient: RecipesClient, private recipesLogged : RecipesLogedClient) { }
  id:any;
  recipe:RecipeDto;
  editableRecipe:any=[];
  
  ngOnInit(): void {
    this.id =  this.sharingService.getData();
    //this.route.params.subscribe(params => {
    this.getThatRecipe();
  }
  getThatRecipe(){
    this.recipesClient.recipes_GetRecipe(this.id).subscribe(res=>(this.recipe = res));
  }
  editRecipe(recipe, file){
    this.recipesLogged.recipesLoged_UploadMultiples(recipe, file).subscribe((res=>this.editableRecipe = res));
  }

}
