import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { APIService } from '../API/api.service';
import { Router } from '@angular/router';

@Injectable({
  providedIn: 'root'
})
export class CardPaymentService {
  private endpoint = 'CardPayment/CreateCardPayment'; // API endpoint

  constructor(private apiService: APIService, private router: Router) {}

  createCardPayment(paymentData: any): Observable<any> {
    return this.apiService.postToEndpoint(this.endpoint, paymentData);
  }
}
