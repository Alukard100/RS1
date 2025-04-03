import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { APIService } from '../API/api.service';
import { LoginResponse } from '../../interfaces/Auth/login-response.interface';
import { LoginRequest } from '../../interfaces/Auth/login-request.interface';
import {VerifyCodeRequest} from '../../interfaces/Auth/verify-code-request.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly loginEndpoint: string = 'UserValues/LoginUser';
  private readonly verifyCodeEndpoint: string = 'UserValues/VerifyCode';

  private loggedIn = new BehaviorSubject<boolean>(this.hasToken());

  constructor(private apiService: APIService, private router: Router) {}

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.apiService.postToEndpoint<LoginResponse>(this.loginEndpoint, request);
  }
  verifyCode(request: VerifyCodeRequest): Observable<LoginResponse> {
    return this.apiService.postToEndpoint<LoginResponse>(this.verifyCodeEndpoint, request);
  }

  setSession(response: any) {
    if (response && response.token) {
      localStorage.setItem('token', response.token);
      localStorage.setItem('userId', response.userId.toString());
      localStorage.setItem('userName', response.userName);
      localStorage.setItem('typeId', response.typeId.toString());
    } else {
      console.error('Invalid login response:', response);
    }
  }

  logout(): void {
    localStorage.clear();
    this.loggedIn.next(false);
    this.router.navigate(['/login']);
  }

  getUserId(): number {
    return Number(localStorage.getItem('userId')) || 0; // Return stored userId as number
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  isLoggedIn(): boolean {
    return this.hasToken();
  }

  getLoggedInStatus(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  private hasToken(): boolean {
    return !!localStorage.getItem('token');
  }

  getUserName(): string {
    return localStorage.getItem('userName') || '';
  }

}
