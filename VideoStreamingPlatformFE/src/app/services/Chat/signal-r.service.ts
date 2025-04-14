import { Injectable } from '@angular/core';
import { HubConnection, HubConnectionBuilder } from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';
import { HttpClient } from '@angular/common/http'; // Import HttpClient to handle API requests
import { Observable } from 'rxjs'; // Ensure Observable is imported

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection: HubConnection | null = null;
  private messagesSubject = new BehaviorSubject<any[]>([]);
  messages$ = this.messagesSubject.asObservable();

  private apiUrl = 'https://localhost:7066';

  constructor(private http: HttpClient) {}

  startConnection(userId: number): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.apiUrl}/chatHub`, {
        accessTokenFactory: () => localStorage.getItem('token') || ''
      })
      .build();

    this.hubConnection.start().then(() => {
      console.log('SignalR connection established');

      this.hubConnection?.invoke('JoinGroup', userId.toString());

      this.hubConnection?.on('ReceiveMessage', (message) => {
        const currentMessages = this.messagesSubject.getValue();
        currentMessages.push(message);
        this.messagesSubject.next(currentMessages); // Update the messages
      });

    }).catch(err => console.error('Error while starting connection: ' + err));
  }

  sendMessage(senderId: number, receiverId: number, messageBody: string): void {
    if (this.hubConnection) {
      this.hubConnection.invoke('SendMessage', senderId, receiverId, messageBody).catch(err => console.error(err));
    }
  }

  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop().catch(err => console.error(err));
    }
  }

  // MORAM DODATI FETCH USERA NA BACKENDU NISAM URADIO
  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/User/GetUsers`);  // Adjust the endpoint as needed
  }

  getMessageBody(senderId: number, receiverId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/MessageBody/GetMessageBody?MsgSenderId=${senderId}&MsgRecieverId=${receiverId}`);
  }
}
