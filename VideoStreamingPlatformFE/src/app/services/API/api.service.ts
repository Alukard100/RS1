import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class APIService {
  private apiUrl = 'https://localhost:7066'; // Base API URL

  constructor(private http: HttpClient) {}

  // Helper function to get headers with authorization token
  private getHeaders(): HttpHeaders {
    const token = localStorage.getItem('token'); // Get token from local storage
    let headers = new HttpHeaders().set('Content-Type', 'application/json');
    if (token) {
      headers = headers.set('Authorization', `Bearer ${token}`);
    }
    return headers;
  }

  // GET request with dynamic endpoint
  getFromEndpoint<T>(endpoint: string): Observable<T> {
    const url = `${this.apiUrl}/${endpoint}`;
    return this.http.get<T>(url, { headers: this.getHeaders() });
  }

  // POST request with dynamic endpoint and body
  postToEndpoint<T>(endpoint: string, data: any): Observable<T> {
    const url = `${this.apiUrl}/${endpoint}`;
    return this.http.post<T>(url, data, { headers: this.getHeaders() });
  }

  // DELETE request with dynamic endpoint and body
  deleteFromEndpoint<T>(endpoint: string, body?: any): Observable<T> {
    const url = `${this.apiUrl}/${endpoint}`;
    return this.http.request<T>('DELETE', url, { body, headers: this.getHeaders() });
  }

  // Generic method for PUT requests
  putToEndpoint<T>(endpoint: string, data: any): Observable<T> {
    const url = `${this.apiUrl}/${endpoint}`;
    return this.http.put<T>(url, data, { headers: this.getHeaders() });
  }

  // Example of a GET request (customized for a specific API route)
  getData(): Observable<any> {
    return this.getFromEndpoint('example-endpoint'); // Adjust endpoint as needed
  }

  // Example of a POST request (customized for a specific API route)
  postData(data: any): Observable<any> {
    return this.postToEndpoint('example-endpoint', data); // Adjust endpoint as needed
  }

  // Getter for API base URL
  getApi(): string {
    return this.apiUrl;
  }
}
