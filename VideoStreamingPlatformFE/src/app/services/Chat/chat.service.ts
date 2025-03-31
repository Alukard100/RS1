import { Injectable } from '@angular/core';
import { APIService } from '../API/api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ChatService {

  constructor(private apiService: APIService) {}

  // Get users for the chat
  getUsers(request: any): Observable<any[]> {
    return this.apiService.getFromEndpoint<any[]>(`GetUsers?userID=${request.userID}`);
  }

  // Create message
  createMessage(messageData: any): Observable<any> {
    return this.apiService.postToEndpoint<any>('MessageBody/CreateMessageBody', messageData);
  }
}
