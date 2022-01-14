import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { AppComponent } from './app.component';
import { HomeComponent } from './home/home.component';
import { MenuComponent } from './menu/menu.component';
import { NotFoundComponent } from './error-pages/not-found/not-found.component';
import { LoginComponent } from './login/login.component';
import { AppRoutingModule } from './app-routing.module';
import { RegisterUserComponent } from './authentication/register-user/register-user.component';
import { MatMenuModule} from '@angular/material/menu';
import { MatButtonModule} from '@angular/material/button' 
import { MatIconModule } from '@angular/material/icon';
import { BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { CommonModule } from '@angular/common';
import { AboutComponent } from './about/about.component';
import { MyRecipesComponent } from './my-recipes/my-recipes.component';
import { RandomRecipeComponent } from './random-recipe/random-recipe.component';
import { BestRecipesComponent } from './best-recipes/best-recipes.component';
import { MatGridListModule } from '@angular/material/grid-list'; 
import {MatExpansionModule} from '@angular/material/expansion'; 
import {MatListModule} from '@angular/material/list'; 

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    MenuComponent,
    NotFoundComponent,
    LoginComponent,
    RegisterUserComponent,
    AboutComponent,
    MyRecipesComponent,
    RandomRecipeComponent,
    BestRecipesComponent,
  ],
  imports: [
    ReactiveFormsModule,
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    CommonModule,
    BrowserAnimationsModule,
    MatMenuModule,
    MatButtonModule,
    MatIconModule,
    MatGridListModule,
    MatExpansionModule,
    MatListModule

    
   
  ],
  exports: [],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }