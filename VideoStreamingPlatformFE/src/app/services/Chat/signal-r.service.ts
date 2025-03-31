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

  private apiUrl = 'https://localhost:7066';  // Replace with your API's URL

  constructor(private http: HttpClient) {}

  // Initialize SignalR connection
  startConnection(userId: number): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl(`${this.apiUrl}/chatHub`, {
        accessTokenFactory: () => localStorage.getItem('token') || ''
      })
      .build();

    // Start the connection
    this.hubConnection.start().then(() => {
      console.log('SignalR connection established');

      // Once connected, join a group specific to the user
      this.hubConnection?.invoke('JoinGroup', userId.toString());

      // Listen for new messages
      this.hubConnection?.on('ReceiveMessage', (message) => {
        const currentMessages = this.messagesSubject.getValue();
        currentMessages.push(message);
        this.messagesSubject.next(currentMessages); // Update the messages
      });

    }).catch(err => console.error('Error while starting connection: ' + err));
  }

  // Send message to the group (user)
  sendMessage(senderId: number, receiverId: number, messageBody: string): void {
    if (this.hubConnection) {
      this.hubConnection.invoke('SendMessage', senderId, receiverId, messageBody).catch(err => console.error(err));
    }
  }

  // Disconnect the SignalR connection
  stopConnection(): void {
    if (this.hubConnection) {
      this.hubConnection.stop().catch(err => console.error(err));
    }
  }

  // Get all users (replace with actual API endpoint)
  getUsers(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/api/users`);  // Adjust the endpoint as needed
  }

  // Get messages between two users (replace with actual API endpoint)
  getMessageBody(senderId: number, receiverId: number): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/api/messages?senderId=${senderId}&receiverId=${receiverId}`);
  }
}
