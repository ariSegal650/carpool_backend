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
import { CheckboxModule } from 'primeng/checkbox';
import { DateEndPipe } from './pipes/date-end.pipe';
import { AsideComponent } from './aside/aside.component';
import { TabMenuModule } from 'primeng/tabmenu';
import { OrganizationProfileComponent } from './organization-profile/organization-profile.component';

const adminRoutes: Routes = [

  {
    path: '',
    component: AsideComponent,
    children: [
      { path: 'dashboard', component: DashboardComponent },
      { path: 'request', component: RequstComponent },
      { path: 'profile', component: OrganizationProfileComponent },
      { path: '', component: DashboardComponent },
    ],
  },


];

@NgModule({
  declarations: [
    DashboardComponent,
    RequstComponent,
    DateEndPipe,
    AsideComponent,
    OrganizationProfileComponent,
  ],
  imports: [
    TabMenuModule,
    SharedModule,
    TableModule,
    CalendarModule,
    CommonModule,
    InputNumberModule,
    InputTextareaModule,
    CheckboxModule,
    RouterModule.forChild(adminRoutes),
  ],
  exports: [RouterModule]
})
export class AdminModule { }
