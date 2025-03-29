import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { APIService } from '../API/api.service';

@Injectable({
  providedIn: 'root'
})
export class SupportService {
  private endpoint = 'Support';

  constructor(private apiService: APIService) {}

  getSupport(userId?: number): Observable<any> {
    const url = userId ? `${this.endpoint}/GetSupport?UserId=${userId}` : `${this.endpoint}/GetSupport`;
    return this.apiService.getFromEndpoint(url);
  }

  createSupport(body: string, userId: number): Observable<any> {
    const payload = {
      Body: body,
      UserId: userId
    };
    return this.apiService.postToEndpoint(`${this.endpoint}/CreateSupport`, payload);
  }

  deleteSupport(supportId: number): Observable<any> {
    const payload = { Id: supportId };
    return this.apiService.deleteFromEndpoint(`${this.endpoint}/DeleteSupport`, payload);
  }
}
