import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { MyRecipesComponent } from './my-recipes.component';

const routes: Routes = [
    { path: '', redirectTo: 'myRecipes', pathMatch: 'full'},
    { path: 'myRecipes', component: MyRecipesComponent },
    { path: '**', redirectTo: 'myRecipes'}
]

@NgModule({  
  imports: 
  [
    RouterModule.forChild(routes),
  ],

  exports: [RouterModule],
  declarations: []  
})
export class MyRecipes { }
