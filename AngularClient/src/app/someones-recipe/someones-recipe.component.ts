import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable, Subscription } from 'rxjs';
import { switchMap } from 'rxjs/operators';
import { RecipePublicListDto, RecipesLogedClient } from '../api/ApiClient';
import { MyRecipesService } from '../shared/services/my-recipes.service';
import { SharingService } from '../shared/sharing.service';

@Component({
  selector: 'app-someones-recipe',
  templateUrl: './someones-recipe.component.html',
  styleUrls: ['./someones-recipe.component.css']
})
export class SomeonesRecipeComponent implements OnInit {


  constructor(private router: Router,
    private recipesService: MyRecipesService,
    private recipeLogged: RecipesLogedClient,
    private route: ActivatedRoute,
    private sharingService: SharingService) { }
  private routeSub: Subscription
  recipes$: Observable<any>;
  userId: any;
  recipes: RecipePublicListDto[];
  respones: any;

  ngOnInit() {
    console.log('b');
    this.recipes$ = this.route.paramMap.pipe(switchMap(params => {
      console.log(params.get('userId'));
      this.userId = (params.get('userId'));
      console.log('c');
      return this.recipesService.someonesRecipe(this.userId);
    }))
    this.recipesService.someonesRecipe(this.userId).subscribe(res => (this.recipes = res));

  }
  viewRecipeDetail(recipe_id: any) {
    let url: string = "/detailsRecipe/" + recipe_id
    this.router.navigateByUrl(url);
    //console.log(recipe_id);
    this.sharingService.setData(recipe_id);
  }

}
