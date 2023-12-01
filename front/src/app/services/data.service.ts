import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { OrganizationInfoDto } from '../models/organization';
import { Observable } from 'rxjs';
import { VerificationDto } from '../models/Verification';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private baseUrl = "https://localhost:7012/api/"
  constructor(private http: HttpClient) { }

  CreateNewOrganization(org: OrganizationInfoDto): Observable<any> {

    return this.http.post(this.baseUrl + "Organization", org);
  }

  AddLogoImage(formData): Observable<any> {

  return this.http.post(this.baseUrl + "Organization/upload", formData);
  }

  BasikPostRequst(requestType, url, body) {
    this.http.post(this.baseUrl + url, body).subscribe({
      next: (value) => {
        console.log(value + " sucsses");
      },
      error(err) {
        console.log("somthing went worng");
      },
    }).unsubscribe();
  }



}
