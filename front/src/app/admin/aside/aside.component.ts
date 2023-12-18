import { Component, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { MenuItem } from 'primeng/api';
import { filter } from 'rxjs';

@Component({
  selector: 'app-aside',
  templateUrl: './aside.component.html',
  styleUrls: ['./aside.component.css']
})
export class AsideComponent  {
  constructor() { }

 
  isMobileMenuOpen: boolean = false;

  desktopMenuItems: MenuItem[] = [
    { label: 'דשבורד', icon: 'pi pi-fw pi-home', routerLink: '/admin/dashboard' },
    { label: 'אזור אישי', icon: 'pi pi-fw pi-calendar', routerLink: '/admin/profile' },
    { label: 'Tab 3', icon: 'pi pi-fw pi-pencil', routerLink: 'tab3' },
  ];

  toggleMobileMenu() {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
  }

}
