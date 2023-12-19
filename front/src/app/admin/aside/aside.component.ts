import { Component, ElementRef, HostListener, OnInit, Renderer2 } from '@angular/core';
import { MenuItem } from 'primeng/api';

@Component({
  selector: 'app-aside',
  templateUrl: './aside.component.html',
  styleUrls: ['./aside.component.css']
})
export class AsideComponent{

  constructor(private renderer: Renderer2,private el: ElementRef) { }
 
  isMobileMenuOpen: boolean = false;

  desktopMenuItems: MenuItem[] = [
    { label: 'דשבורד', icon: 'pi pi-fw pi-home', routerLink: '/admin/dashboard' },
    { label: 'אזור אישי', icon: 'pi pi-fw pi-calendar', routerLink: '/admin/profile' },
    { label: 'Tab 3', icon: 'pi pi-fw pi-pencil', routerLink: 'tab3' },
  ];


  toggleMobileMenu() {
    this.isMobileMenuOpen = !this.isMobileMenuOpen;
  }

  @HostListener('document:click', ['$event'])
  checkClickOutside(event: Event) {
    const clickedElement = event.target as HTMLElement;

    // Check if the clicked element is not part of the mobile menu and its parent container
    if (!clickedElement.closest('.aside-container') && !clickedElement.classList.contains('mobile-menu-trigger')) {
      // Clicked outside the mobile menu, so close it
      this.isMobileMenuOpen = false;
      console.log('Closed mobile menu');
    }
    
  }


}
