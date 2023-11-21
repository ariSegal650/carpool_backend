import { NgModule } from '@angular/core';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RouterModule, Routes } from '@angular/router';
import { RequstComponent } from './requst/requst.component';
import { SharedModule } from '../shared.module';
import { TableModule } from 'primeng/table';
import { CalendarModule } from 'primeng/calendar';
import { CommonModule } from '@angular/common';
import { InputNumberModule } from 'primeng/inputnumber';
import { InputTextareaModule } from 'primeng/inputtextarea';

const adminRoutes: Routes = [

  { path: 'dashboard', component: DashboardComponent },
  { path: 'request', component: RequstComponent },
  { path: '', component: DashboardComponent },
];

@NgModule({
  declarations: [
    DashboardComponent,
    RequstComponent,
    
  ],
  imports: [
    SharedModule,
    TableModule,
    CalendarModule,
    CommonModule,
    InputNumberModule,
    InputTextareaModule,
    RouterModule.forChild(adminRoutes)
  ],
  exports: [RouterModule]
})
export class AdminModule { }
