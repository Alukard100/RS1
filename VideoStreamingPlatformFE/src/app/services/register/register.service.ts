import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { APIService } from '../API/api.service';

@Injectable({
  providedIn: 'root'
})
export class RegisterService {
  private apiUrl = 'User/CreateUser/'; // Replace with actual API URL

  constructor(private http: HttpClient, private apiService: APIService) {}

  register(request1: any): Observable<any> {
    // The data is already in the correct format, no need to transform
    const request = {
      name: request1.name,
      surname: request1.surname,
      userName: request1.userName,
      email: request1.email,
      password: request1.password,
      birthDate: request1.birthDate,
      country: request1.country,
      profilePicture: null
    };

    alert(request.name);
    return this.apiService.postToEndpoint<any>(this.apiUrl, request);
  }
}
