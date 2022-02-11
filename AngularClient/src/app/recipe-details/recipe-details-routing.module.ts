import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { RecipeDetailsComponent } from './recipe-details.component';


const routes: Routes = [
    { path: '', redirectTo: 'detailsRecipe', pathMatch: 'full'},
    { path: 'detailsRecipe', component: RecipeDetailsComponent },
    { path: '**', redirectTo: 'detailsRecipe'}
]

@NgModule({  
  imports: 
  [
    RouterModule.forChild(routes),
  ],

  exports: [RouterModule],
  declarations: []  
})
export class RecipeDetails { }
