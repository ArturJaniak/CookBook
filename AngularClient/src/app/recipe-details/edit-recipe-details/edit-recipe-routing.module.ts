import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EditRecipeDetailsComponent } from './edit-recipe-details.component';


const routes: Routes = [
    { path: '', redirectTo: 'editDetailsRecipe', pathMatch: 'full'},
    { path: 'editDetailsRecipe', component: EditRecipeDetailsComponent },
    { path: '**', redirectTo: 'editDetailsRecipe'}
]

@NgModule({  
  imports: 
  [
    RouterModule.forChild(routes),
  ],

  exports: [RouterModule],
  declarations: []  
})
export class EditRecipeDetails { }
