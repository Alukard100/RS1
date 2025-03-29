import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class APIService {
  private apiUrl = 'https://localhost:7066'; // Base URL
  constructor(private http: HttpClient) { }

  // Example GET request
  getData(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  // Example POST request
  postData(data: any): Observable<any> {
    return this.http.post(this.apiUrl, data);
  }

  //For registration and login we use that route, everything about user values is on that destination

  // loginUser(data: any): Observable<any> {
  //   const authUrl = 'https://localhost:7066/UserValues'; // Corrected endpoint
  //   return this.http.post(authUrl, data); // Using 'authUrl' for login
  // }

  // Generic POST method to any endpoint
  postToEndpoint<T>(endpoint: string, data: any): Observable<T> {
    const url = `${this.apiUrl}/${endpoint}`;
    return this.http.post<T>(url, data);
  }

  getFromEndpoint<T>(endpoint: string): Observable<T> {
    const url = `${this.apiUrl}/${endpoint}`;
    return this.http.get<T>(url);
  }

  deleteFromEndpoint<T>(endpoint: string, body?: any): Observable<T> {
    const url = `${this.apiUrl}/${endpoint}`;
    return this.http.request<T>('DELETE', url, { body });
  }


  // Getter for API base URL
  getApi(): string {
    return this.apiUrl;
  }
}
