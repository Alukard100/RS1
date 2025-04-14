import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { APIService } from '../API/api.service';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: HubConnection | null = null;
  private messagesSubject = new BehaviorSubject<any[]>([]);
  messages$ = this.messagesSubject.asObservable();

  constructor(private apiService: APIService) {}

  // Start the SignalR connection
  startConnection(userId: number): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.apiService.getApi()}/chatHub`, {
        accessTokenFactory: () => localStorage.getItem('token') || ''
      })
      .build();

    this.hubConnection
      .start()
      .then(() => {
        console.log('SignalR connection established');

        this.hubConnection?.invoke('JoinGroup', userId.toString());

        this.hubConnection?.on('ReceiveMessage', (message) => {
          const currentMessages = this.messagesSubject.getValue();
          this.messagesSubject.next([...currentMessages, message]);
        });
      })
      .catch(err => console.error('SignalR connection error:', err));
  }

  sendMessage(senderId: number, receiverId: number, messageBody: string): void {
    if (!senderId || !receiverId || !messageBody) return;

    const messageRequest = {
      MsgSenderId: senderId,
      MsgRecieverId: receiverId,
      Body: messageBody
    };

    // Make POST request to the backend to save the message
    this.apiService.postToEndpoint('MessageBody/CreateMessageBody', messageRequest)
      .subscribe({
        next: (response) => {
          // Now use SignalR to notify the client (it’s automatically handled in backend)
          if (this.hubConnection) {
            this.hubConnection
              .invoke('SendMessage', senderId, receiverId, messageBody)
              .catch(err => console.error('Error sending message:', err));
          }
        },
        error: (err) => {
          console.error('Error sending message to API:', err);
        }
      });
  }

  // Stop SignalR connection
  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop().catch(err => console.error('Error stopping connection:', err));
    }
  }

  // REST calls moved to APIService through injection
  getUsers(): Observable<any[]> {
    return this.apiService.getFromEndpoint<any[]>('User/GetUsers');
  }

  getMessageBody(senderId: number, receiverId: number): Observable<any[]> {
    return this.apiService.getFromEndpoint<any[]>(
      `MessageBody/GetMessageBody?MsgSenderId=${senderId}&MsgRecieverId=${receiverId}`
    );
  }
}
