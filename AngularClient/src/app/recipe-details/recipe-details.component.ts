import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { RecipeLogedViewClient, RecipesClient, RecipesLogedClient } from '../api/ApiClient';
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
      console.log("recipe "+ this.recipe + " "+ this.recipe.rating);
      this.recipesLogged.recipesLoged_RateRecipe(localStorage.getItem("token"), this.id, rateValue).subscribe(res=>(this.recipe.rating) = res);
      console.log("recipe rating" + this.recipe.rating +" "+ this.id);
      this.ngOnInit();
      //this.getThatRecipe();
    
  }
  getThatRecipe(){
    this.recipesClient.recipes_GetRecipe(this.id).subscribe(res=>(this.recipe = res));
  }
  
}