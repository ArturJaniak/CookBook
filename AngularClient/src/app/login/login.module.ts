import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { LoginComponent } from './login.component';

const routes: Routes = [
    { path: '', redirectTo: 'login', pathMatch: 'full'},
    { path: 'login', component: LoginComponent },
    { path: '**', redirectTo: 'login'}
]

@NgModule({  
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
  declarations: []  
})
export class LoginModule { }
