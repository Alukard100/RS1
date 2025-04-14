import { Component, OnInit, OnDestroy } from '@angular/core';
import { SignalRService } from '../../services/Chat/signal-r.service';
import { AuthService } from '../../services/Auth/auth.service'; // To get the logged-in user
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
  standalone:false,
})
export class ChatComponent implements OnInit, OnDestroy {
  users: any[] = [];
  selectedUserId: number | null = null;
  loggedInUserId!: number;
  messageBody: string = '';
  messages: any[] = [];

  private messageSubscription: Subscription | null = null;

  constructor(
    private signalRService: SignalRService,
    private authService: AuthService
  ) {}

  ngOnInit(): void {
    const user = this.authService.getCurrentUser();
    this.loggedInUserId = user?.userId;

    // Start SignalR connection
    this.signalRService.startConnection(this.loggedInUserId);

    // Subscribe to incoming messages
    this.messageSubscription = this.signalRService.messages$.subscribe((msgs) => {
      this.messages = [...msgs];
    });

    // Load user list
    this.signalRService.getUsers().subscribe((data) => {
      this.users = data.filter(u => u.userId !== this.loggedInUserId);
    });
  }

  // Called when a user is selected
  selectUser(userId: number): void {
    this.selectedUserId = userId;
    // Load messages between current user and selected user
    this.signalRService.getMessageBody(this.loggedInUserId, userId).subscribe((data) => {
      this.messages = data;
    });
  }

  // Send a message
  sendMessage(): void {
    if (this.messageBody.trim() && this.selectedUserId !== null) {
      this.signalRService.sendMessage(this.loggedInUserId, this.selectedUserId, this.messageBody);
      this.messageBody = '';
    }
  }

  ngOnDestroy(): void {
    this.signalRService.stopConnection();
    this.messageSubscription?.unsubscribe();
  }
}
