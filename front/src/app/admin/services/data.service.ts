import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { RequestAdmin } from 'src/app/admin/models/request';


@Injectable({
  providedIn: 'root'
})
export class DataService {

  private baseUrl = "https://localhost:7012/api/"

  constructor(private http: HttpClient) { }

  getAllRequsr(): Observable<Array<RequestAdmin>> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    });
    return this.http.get<Array<RequestAdmin>>(this.baseUrl + "Requst", { headers });
  }

  addReqqust(requestAdmin:RequestAdmin): Observable<Array<RequestAdmin>> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${localStorage.getItem('token')}`
    });
    return this.http.post<Array<RequestAdmin>>(this.baseUrl + "Requst",requestAdmin, { headers });
  }
}
