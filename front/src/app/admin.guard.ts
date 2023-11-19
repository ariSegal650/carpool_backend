
import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Route, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { VerificationService } from './services/verification.service';

@Injectable({
  providedIn: 'root'
})
export class AdminAuthGuard implements CanActivate {

  constructor(private _verificationService: VerificationService, private router: Router) { }

  async canActivate(): Promise<boolean> {
    try {
      const response = await this._verificationService.validateJwt().toPromise();

      console.log(response.message);

      return true;
    } catch (error) {
      console.log(error);
      
      // Handle the error, e.g., redirect to login page
      this.router.navigate(['/login']);
      return false;
    }
  }
}