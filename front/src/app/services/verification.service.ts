import { Injectable } from '@angular/core';
import { VerificationDto } from '../models/Verification';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class VerificationService {

  private baseUrl = "https://localhost:7012/api/"

  constructor(private http: HttpClient) { }

  async GetVerification(verificationfile:VerificationDto):Promise<boolean>{
    
    await this.http.post(this.baseUrl+"Verification",verificationfile).subscribe({
       next:(value)=> {
         console.log(value);

         return true;         
       },
     })

     return false;
   }

   async CheckCode(verificationfile:VerificationDto):Promise<boolean>{

    await this.http.post(this.baseUrl+"Verification/Check",verificationfile).subscribe({
      next:(value)=> {
        console.log(value);

        return true;         
      },
    })

    return false;
  }
   }

