import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RandomRecipeComponent } from './random-recipe.component';


const routes: Routes = [
    { path: '', redirectTo: 'randomRecipe', pathMatch: 'full'},
    { path: 'randomRecipe', component: RandomRecipeComponent },
    { path: '**', redirectTo: 'randomRecipe'}
]

@NgModule({  
  imports: 
  [
    RouterModule.forChild(routes),
  ],

  exports: [RouterModule],
  declarations: []  
})
export class RandomRecipe { }
