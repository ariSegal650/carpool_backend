import { Injectable } from '@angular/core';
import { VerificationDto } from '../models/Verification';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class VerificationService {

  private baseUrl = "https://localhost:7012/api/"

  constructor(private http: HttpClient,private router: Router) { }

  async GetVerification(verificationfile: VerificationDto): Promise<boolean> {
    console.log(verificationfile);

    await this.http.post(this.baseUrl + "Verification", verificationfile).subscribe({
      next: (value) => {
        console.log(value);
        return true;
      },
      error: e => {
        console.log(e);

      }
    })
    return false;
  }

  async CheckCode(verificationfile: VerificationDto): Promise<boolean> {
    var response: boolean;
    await this.http.post<any>(this.baseUrl + "Verification/Check", verificationfile).subscribe({
      next: (value) => {
        console.log(value);
        localStorage.setItem("token", value?.token);
        this.router.navigate(['/admin'])
        response = true;
      },
      error: e => {
        return false;
      }
    })
    return response;

  }

  validateJwt(): Observable<any> {
    
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    });
    
    return this.http.get<any>(this.baseUrl + "Verification", { headers });
  }

  LoginAdmin(LoginForm): Observable<any> {
    return this.http.post<any>(this.baseUrl + "Verification", LoginForm);

  }
}

