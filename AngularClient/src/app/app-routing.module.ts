import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router'
import { AboutComponent } from './about/about.component';
import { RegisterUserComponent } from './authentication/register-user/register-user.component';
import { BestRecipesComponent } from './best-recipes/best-recipes.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component'
import { HomeComponent } from './home/home.component'
import { LoginComponent } from './login/login.component';
import { MyRecipesComponent } from './my-recipes/my-recipes.component';
import { RandomRecipeComponent } from './random-recipe/random-recipe.component';
import { RecipeDetailsComponent } from './recipe-details/recipe-details.component';

const routes: Routes = [
    { path: '', component: HomeComponent,
children: [
    { path: 'company', loadChildren: () => import('./company/company.module').then(m => m.CompanyModule) },
    
]},
    { path: '404', component : NotFoundComponent},
    { path: 'login', component: LoginComponent},
    { path: 'home', component: HomeComponent},
    { path: 'about', component: AboutComponent},
    { path: 'myRecipes', component: MyRecipesComponent},
    { path: 'detailsRecipe', component: RecipeDetailsComponent, children:[
        { path: ':id', component: RecipeDetailsComponent},
    ]},
    { path: 'randomRecipe', component: RandomRecipeComponent},
    { path: 'bestRecipes', component: BestRecipesComponent},
    
    { path: 'authentication/register', component: RegisterUserComponent},
    { path: '**', redirectTo: '/404', pathMatch: 'full'},
];
@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModule{}
    
