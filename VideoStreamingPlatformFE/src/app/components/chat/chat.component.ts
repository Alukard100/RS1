import { Component, OnInit, OnDestroy } from '@angular/core';
import { SignalRService } from '../../services/Chat/signal-r.service';
import { GetUserResponse} from '../../interfaces/get-user-response.model';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
  standalone:false,
})
export class ChatComponent implements OnInit, OnDestroy {
  users: any[] = [];
  messages: any[] = [];
  selectedUserId: number | null = null;
  selectedUserName:string='';
  messageBody: string = '';
  loggedInUserId: number = 0;

  constructor(private signalRService: SignalRService) {}

  ngOnInit(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.loggedInUserId = +userId;
      this.signalRService.startConnection(this.loggedInUserId);
    }

    this.loadUsers();

    this.signalRService.messages$.subscribe((msgs) => {
      console.log('Received messages:', msgs);
      if (this.selectedUserId !== null) {
        this.messages = msgs.filter(
          msg =>
            (msg.msgSenderId === this.loggedInUserId && msg.msgRecieverId === this.selectedUserId) ||
            (msg.msgSenderId === this.selectedUserId && msg.msgRecieverId === this.loggedInUserId)
        );
      }
    });
  }

  loadUsers(): void {
    this.signalRService.getUsers().subscribe((data) => {
      this.users = data;
    });
  }

  selectUser(user: GetUserResponse): void {
    this.selectedUserId = user.userID ?? null;
    this.selectedUserName = user.userName ?? '';
    if (this.selectedUserId !== null) {
      this.getMessages(this.loggedInUserId, this.selectedUserId);
    }
  }

  getMessages(senderId: number, receiverId: number): void {
    if (!receiverId) return;

    this.signalRService.getMessageBody(senderId, receiverId).subscribe({
      next: (data) => {
        this.messages = data;
      },
      error: (err) => {
        console.error('Failed to fetch messages:', err);
      },
    });
  }


  sendMessage(): void {
    if (this.messageBody.trim() && this.selectedUserId !== null) {
      this.signalRService.sendMessage(this.loggedInUserId, this.selectedUserId, this.messageBody);
      this.messageBody = '';
    }
  }

  ngOnDestroy(): void {
    this.signalRService.stopConnection();
  }
}
