import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NewOrganizationComponent } from './components/new-organization/new-organization.component';
import { AdminAuthGuard } from './admin.guard';
import { LoginComponent } from './components/login/login.component';

const routes: Routes = [
  { path: '', component: NewOrganizationComponent },
  { path: "login", component: LoginComponent },
  
  {
    path: 'admin',
    loadChildren: () => import('./admin/admin.module').then((m) => m.AdminModule),
    canActivate: [AdminAuthGuard],
  },
  { path: '**', redirectTo:"" },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
