import { Injectable } from '@angular/core';
import {BehaviorSubject, Observable} from 'rxjs';
import {HttpClient} from '@angular/common/http';
import {APIService} from '../API/api.service';
import {Router} from '@angular/router';
import {LoginResponse} from '../../interfaces/Auth/login-response.interface';
import {request, response} from 'express';
import {LoginRequest} from '../../interfaces/Auth/login-request.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  private readonly loginEndpoint: string = 'UserValues/LoginUser';
  private loggedIn= new BehaviorSubject<boolean>(this.hasToken());
  constructor(
    private apiService: APIService,
    private router: Router,
  ) {}

  login(request: LoginRequest): Observable<LoginResponse> {
    return this.apiService.postToEndpoint<LoginResponse>(this.loginEndpoint, request);
  }


  setSession(response:LoginResponse):void{
    localStorage.setItem('token',response.Token);
    localStorage.setItem('userId',response.UserId);
    localStorage.setItem('userName',response.UserName);
    localStorage.setItem('typeId',response.TypeId);
    this.loggedIn.next(true);
  }

  logout():void{
    localStorage.clear();
    this.loggedIn.next(false);
    this.router.navigate(['/login']);
  }

  getToken():string|null{
    return localStorage.getItem('token');
  }

  isLoggedIn():boolean{
    return this.hasToken();
  }

  getLoggedInStatus():Observable<boolean>{
    return this.loggedIn.asObservable();
  }

  private hasToken():boolean {
    return !!localStorage.getItem('token');
  }
}
