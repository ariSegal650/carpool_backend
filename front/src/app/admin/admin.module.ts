import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RouterModule, Routes } from '@angular/router';

const adminRoutes: Routes = [
  
  { path: '', component: DashboardComponent },
  { path: 'dashboard', component: DashboardComponent },

];

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [RouterModule.forChild(adminRoutes)],
  exports: [RouterModule]
})
export class AdminModule { }
