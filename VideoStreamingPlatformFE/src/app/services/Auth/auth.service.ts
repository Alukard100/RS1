import { Inject, Injectable, PLATFORM_ID } from '@angular/core';
import { BehaviorSubject, Observable } from 'rxjs';
import { Router } from '@angular/router';
import { APIService } from '../API/api.service';
import { LoginResponse, VerifiedCodeResponse } from '../../interfaces/Auth/login-response.interface';
import { LoginRequest } from '../../interfaces/Auth/login-request.interface';
import { VerifyCodeRequest } from '../../interfaces/Auth/verify-code-request.interface';
import { isPlatformBrowser } from '@angular/common';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly loginEndpoint: string = 'UserValues/LoginUser';
  private readonly verifyCodeEndpoint: string = 'UserValues/VerifyCode';

  public loggedIn: BehaviorSubject<boolean>;
  private isBrowser: boolean;

  constructor(
    private apiService: APIService,
    private router: Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(this.platformId);
    this.loggedIn = new BehaviorSubject<boolean>(this.hasToken());
  }

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.apiService.postToEndpoint<LoginResponse>(this.loginEndpoint, request);
  }

  verifyCode(request: VerifyCodeRequest): Observable<VerifiedCodeResponse> {
    return this.apiService.postToEndpoint<VerifiedCodeResponse>(this.verifyCodeEndpoint, request);
  }

  setSession(response: any): void {
    if (this.isBrowser) {
      localStorage.setItem('token', response.token);
      localStorage.setItem('userId', response.userId.toString());
      localStorage.setItem('userName', response.userName);
      localStorage.setItem('typeId', response.typeId.toString());

      this.loggedIn.next(true);
    }
  }

  getCurrentUser() {
    if (this.isBrowser) {
      const userStr = localStorage.getItem('user');
      return userStr ? JSON.parse(userStr) : null;
    }
    return null;
  }

  logout(): void {
    if (this.isBrowser) {
      localStorage.removeItem('token');
      localStorage.removeItem('userId');
      localStorage.removeItem('userName');
      localStorage.removeItem('typeId');
    }

    this.loggedIn.next(false);
    this.router.navigate(['/login']);
  }

  getUserId(): number {
    if (this.isBrowser) {
      return Number(localStorage.getItem('userId')) || 0;
    }
    return 0;
  }

  getToken(): string | null {
    if (this.isBrowser) {
      return localStorage.getItem('token');
    }
    return null;
  }

  private hasToken(): boolean {
    if (this.isBrowser) {
      return !!localStorage.getItem('token');
    }
    return false;
  }

  getLoggedInStatus(): Observable<boolean> {
    return this.loggedIn.asObservable();
  }

  getUserName(): string {
    if (this.isBrowser) {
      return localStorage.getItem('userName') || '';
    }
    return '';
  }

  getLoggedInStatusSync(): boolean {
    if (this.isBrowser) {
      return !!localStorage.getItem('token');
    }
    return false;
  }
}
