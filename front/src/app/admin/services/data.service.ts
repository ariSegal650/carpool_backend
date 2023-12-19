import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, catchError, throwError } from 'rxjs';
import { RequestAdmin } from 'src/app/admin/models/request';
import { OrganizationInfoDto } from 'src/app/models/organization';


@Injectable({
  providedIn: 'root'
})
export class DataService {

  private baseUrl = " https://localhost:7012/api/"

  constructor(private http: HttpClient, private router: Router) { }

  getAllRequsr(): Observable<Array<RequestAdmin>> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    });
    return this.http.get<Array<RequestAdmin>>(this.baseUrl + 'Requst', { headers });


  }

  addReqqust(requestAdmin: RequestAdmin): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    });
    return this.http.post<any>(this.baseUrl + "Requst", requestAdmin, { headers })
      .pipe(
        catchError((error: HttpErrorResponse) => {
          if (error.status === 401) {
            this.router.navigate(['/login']);
            console.log(error.status);
          }

          return throwError(error);
        })
      );
  }

  getOrganization() {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    });
    return this.http.get<OrganizationInfoDto>(this.baseUrl + "Organization", { headers });
  }

  updateOrganization(org: OrganizationInfoDto) {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    });
    return this.http.put<OrganizationInfoDto>(this.baseUrl + "Organization", org, { headers });
  }
}
