import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { APIService } from '../API/api.service';
import { Router } from '@angular/router';
import {HttpClient} from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class CardPaymentService {
  private endpoint = 'CardPayment/CreateCardPayment'; // API endpoint

  constructor(private apiService: APIService, private router: Router, private http: HttpClient) {}

  createCardPayment(paymentData: any): Observable<any> {
    return this.apiService.postToEndpoint(this.endpoint, paymentData);
  }
  enterPromoCode(request: { userId: number; codeValue: string }): Observable<any> {
    return this.apiService.postToEndpoint('Wallet/EnterPromoCode', request);
  }

  getWallet(userId: number) {
    return this.apiService.getFromEndpoint<{ balance: number }>(
      `Wallet/GetWallet?UserId=${userId}`
    );
  }

}
