import { Injectable } from '@angular/core';
import { APIService } from '../API/api.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class CategoryService {

  constructor(private apiService: APIService, private http: HttpClient) { }
  

  fetchCategory(id : number = 2): Observable<any> {
    return this.http.get<any>(`${this.apiService.getApi()}/Category/GetCategory?CategoryId=${id}`);
  }

}
