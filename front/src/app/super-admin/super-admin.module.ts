import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule, Routes } from '@angular/router';
import { SharedModule } from '../shared.module';
import { TableModule } from 'primeng/table';
import { DashboardComponent } from './components/dashboard/dashboard.component';


const SuperAdminRoutes: Routes = [
];

@NgModule({
  declarations: [
    DashboardComponent
  ],
  imports: [
    CommonModule,
    SharedModule,
    RouterModule,
    TableModule,
    RouterModule.forChild(SuperAdminRoutes),
  ],
  exports: [RouterModule]
})
export class SuperAdminModule { }
