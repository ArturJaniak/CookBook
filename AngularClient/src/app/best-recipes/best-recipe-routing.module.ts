import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { BestRecipesComponent } from './best-recipes.component';
import { MatMenuModule } from '@angular/material/menu';

const routes: Routes = [
    { path: '', redirectTo: 'bestRecipes', pathMatch: 'full'},
    { path: 'bestRecipes', component: BestRecipesComponent },
    { path: '**', redirectTo: 'myrecipes'}
]

@NgModule({  
  imports: 
  [
    RouterModule.forChild(routes),
    MatMenuModule,
  ],

  exports: [RouterModule],
  declarations: []  
})
export class BestRecipe { }
