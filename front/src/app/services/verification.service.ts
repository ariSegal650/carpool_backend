import { Injectable } from '@angular/core';
import { VerificationDto } from '../models/Verification';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VerificationService {

  private baseUrl = "/api/"

  constructor(private http: HttpClient) { }

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

   CheckCode(verificationfile: VerificationDto):Observable<any> {

    return this.http.post<any>(this.baseUrl + "Verification/Check", verificationfile);
    
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

