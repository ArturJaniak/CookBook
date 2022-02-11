import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { RecipePublicListDto, RecipesLogedClient } from '../api/ApiClient';
import { MyRecipesService } from '../shared/services/my-recipes.service';

@Component({
  selector: 'app-my-recipes',
  templateUrl: './my-recipes.component.html',
  styleUrls: ['./my-recipes.component.css']
})
export class MyRecipesComponent implements OnInit {

 

  constructor(private router: Router,private recipesService: MyRecipesService, private recipeLogged: RecipesLogedClient, private route:ActivatedRoute) {}
  recipes$: Observable<any>;
  selectedId: any;
  recipes : RecipePublicListDto[];

  token: string;
  typesOfShoes: string[] = ['Boots', 'Clogs', 'Loafers', 'Moccasins', 'Sneakers'];
  respones:any;

  ngOnInit() {
    this.recipes$ = this.route.paramMap.pipe(switchMap(params => {
      this.selectedId = Number(params.get('id'));
      return this.recipesService.getRecipes();
    }))
    this.recipesService.getRecipes().subscribe(res=>(this.recipes = res));
    
  }
  viewRecipeDetail(recipe_id : any){
   let url: string = "/detailsRecipe/" + recipe_id
        this.router.navigateByUrl(url);
     }
  createRecipe(){
    alert("dziala");
    this.recipeLogged.recipesLoged_CreateRecipe(localStorage.getItem("token")).subscribe(res=>(this.respones = res));
  }
  deleteRecipe(id : any){
    this.recipeLogged.recipesLoged_DeleteConfirmed(id, localStorage.getItem("token")).subscribe(res=>(this.respones=res));
  }
  // createRecipe(){
  //   this.recipesService.createRecipe();
  //   //alert("dziala");
   
  // }

}


