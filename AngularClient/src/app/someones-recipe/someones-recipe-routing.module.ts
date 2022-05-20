import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { SomeonesRecipeComponent } from './someones-recipe.component';


const routes: Routes = [
    { path: '', redirectTo: 'someonesRecipe', pathMatch: 'full' },
    { path: 'someonesRecipe', component: SomeonesRecipeComponent },
    { path: '**', redirectTo: 'someonesRecipe' }
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
