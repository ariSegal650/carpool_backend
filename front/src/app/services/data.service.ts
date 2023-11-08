import { Injectable } from '@angular/core';
import { RequestOrg } from '../models/request';
import { HttpClient } from '@angular/common/http';
import { OrganizationInfoDto } from '../models/organization';

@Injectable({
  providedIn: 'root'
})
export class DataService {

  private baseUrl = "https://localhost:7012/api/"
  constructor(private http: HttpClient) { }

  SendRequest(request: RequestOrg) {
    this.BasikPostRequst("post", "User", request);
  }
  CreateNewOrganization(org: OrganizationInfoDto) {

    this.BasikPostRequst("post", "Organization", org);
  }

  BasikPostRequst(requestType, url, body) {
    this.http.post(this.baseUrl + url, body).subscribe({
      next: (value) => {
        console.log(value + " sucsses");
      },
      error(err) {
        console.log("somthing went worng");
      },
    })
  }
}
