import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable} from 'rxjs';
import { RecipesClient, RecipesLogedClient } from '../api/ApiClient';
import { SharingService } from '../shared/sharing.service';

@Component({
  selector: 'app-recipe-details',
  templateUrl: './recipe-details.component.html',
  styleUrls: ['./recipe-details.component.css']
})
export class RecipeDetailsComponent implements OnInit {

  constructor(private recipesClient: RecipesClient,
     private sharingService: SharingService,
     private recipesLogged: RecipesLogedClient,
     private router: Router,
     ){}
  recipe$ : Observable<any>;
  id:any;
  recipe:any=[];




  rateValue:number;
  rating : number;
  rate:any
  newRecipe:any=[];
  ngOnInit(): void {
    this.id =  this.sharingService.getData();
    //this.route.params.subscribe(params => {
    this.getThatRecipe();
    //})
    
    }
  rateRecipe(rateValue){
    //this.id = this.id;
      this.rateValue = rateValue;
     // console.log("recipe "+ this.recipe + " "+ this.recipe.rating);
      this.recipesLogged.recipesLoged_RateRecipe(localStorage.getItem("token"), this.id, rateValue).subscribe(res=>(this.recipe.rating) = res);
      //console.log("recipe rating" + this.recipe.rating +" "+ this.id);
      //this.ngOnInit();
      //this.getThatRecipe();
    
  }
  getThatRecipe(){
    this.recipesClient.recipes_GetRecipe(this.id).subscribe(res=>(this.recipe = res));
  }
  editRecipeDetail(recipe_id : any){
    let url: string = "/editDetailsRecipe/" + recipe_id
         this.router.navigateByUrl(url);
         //console.log(recipe_id);
         this.sharingService.setData(recipe_id);
      }
  
}