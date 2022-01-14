import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { ReactiveFormsModule } from '@angular/forms';
import { AboutComponent } from './about.component';
import { MatMenuModule } from '@angular/material/menu';

const routes: Routes = [
    { path: '', redirectTo: 'about', pathMatch: 'full'},
    { path: 'about', component: AboutComponent },
    { path: '**', redirectTo: 'about'}
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
export class AboutModule { }
