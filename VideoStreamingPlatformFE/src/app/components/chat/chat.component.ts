import { Component, OnInit, OnDestroy } from '@angular/core';
import { SignalRService } from '../../services/Chat/signal-r.service';
import { ActivatedRoute } from '@angular/router';
import { AuthService } from '../../services/Auth/auth.service'; // Import your AuthService to get the logged-in user

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
  standalone:false
})
export class ChatComponent implements OnInit, OnDestroy {
  users: any[] = [];
  selectedUserId: number | null = null;
  loggedInUserId!: number; // To store the logged-in user's ID
  messageBody: string = '';
  messages: any[] = []; // Store conversation messages

  constructor(
    private signalRService: SignalRService,
    private route: ActivatedRoute,
    private authService: AuthService // Inject AuthService
  ) {}

  ngOnInit(): void {
    // Fetch logged-in userId from AuthService
    this.loggedInUserId = this.authService.getUserId(); // Assuming your AuthService has a method like getUserId()

    this.getUsers();

    // Subscribe to route params to get the selected userId
    this.route.params.subscribe(params => {
      this.selectedUserId = +params['userId'];  // Fetch the userId from route params
      this.getMessages();
      this.signalRService.startConnection(this.selectedUserId);  // Start SignalR connection for real-time messaging
    });

    // Subscribe to SignalR messages
    this.signalRService.messages$.subscribe(newMessages => {
      this.messages = newMessages; // Update the messages array with new messages from SignalR
    });
  }

  ngOnDestroy(): void {
    this.signalRService.stopConnection(); // Stop SignalR connection when the component is destroyed
  }

  // Fetch users
  getUsers(): void {
    this.signalRService.getUsers().subscribe(users => {
      this.users = users;
    });
  }

  // Fetch the conversation messages for the selected user from the server (only needed for initial load)
  getMessages(): void {
    if (this.selectedUserId) {
      this.signalRService.getMessageBody(this.loggedInUserId, this.selectedUserId).subscribe(messages => {
        this.messages = messages;
      });
    }
  }

  // Create message and send it using SignalR for real-time updates
  sendMessage(): void {
    if (this.selectedUserId && this.messageBody) {
      const messageData = {
        MsgSenderId: this.loggedInUserId,
        MsgRecieverId: this.selectedUserId,
        Body: this.messageBody,
        TimeSent: new Date().toISOString(),
        Seen: false
      };

      // Send message using SignalR service
      this.signalRService.sendMessage(
        this.loggedInUserId,
        this.selectedUserId,
        this.messageBody
      );

      // Optionally, update messages locally for instant UI feedback
      const newMessage = { ...messageData, timeSent: new Date().toISOString() };
      this.messages.push(newMessage);
      this.messageBody = ''; // Clear the message input after sending
    }
  }

  // Handle user selection from the chat list
  selectUser(userId: number): void {
    this.selectedUserId = userId;
    this.getMessages(); // Fetch messages once user is selected
    this.signalRService.startConnection(userId);  // Start SignalR connection for real-time messaging
  }
}
