import { Component, OnInit, OnDestroy, NgZone } from '@angular/core';
import { SignalRService } from '../../services/Chat/signal-r.service';
import { GetUserResponse } from '../../interfaces/get-user-response.model';
import { Subscription } from 'rxjs';
import { ChangeDetectorRef } from '@angular/core';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
  standalone: false
})
export class ChatComponent implements OnInit, OnDestroy {
  users: any[] = [];
  messages: any[] = [];
  messageMap: { [userId: number]: any[] } = {};
  selectedUserId: number | null = null;
  selectedUserName: string = '';
  messageBody: string = '';
  loggedInUserId: number = 0;
  private newMsgSub?: Subscription;

  constructor(
    private signalRService: SignalRService,
    private ngZone: NgZone,
    private cdr: ChangeDetectorRef
  ) {}

  ngOnInit(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.loggedInUserId = +userId;
      this.signalRService.startConnection(this.loggedInUserId);
    }

    this.loadUsers();

    this.newMsgSub = this.signalRService.newMessage$.subscribe((msg: any) => {
      console.log('DEBUG: msg:', msg);
      console.log('selectedUserId:', this.selectedUserId);
      console.log('loggedInUserId:', this.loggedInUserId);

      const chatPartnerId = msg.senderId === this.loggedInUserId
        ? msg.receiverId
        : msg.senderId;

      // Ensure we always initialize the message map
      if (!this.messageMap[chatPartnerId]) {
        this.messageMap[chatPartnerId] = [];
      }

      // Create a properly formatted message object
      const formattedMessage = {
        msgSenderId: msg.senderId,
        msgRecieverId: msg.receiverId,
        body: msg.body,
        timeSent: msg.timeSent
      };

      // Push to correct conversation
      this.messageMap[chatPartnerId].push(formattedMessage);

      // If the user is chatting with this partner, update UI
      if (this.selectedUserId === chatPartnerId) {
        this.ngZone.run(() => {
          this.messages = [...this.messageMap[chatPartnerId]];
          this.cdr.detectChanges();
          setTimeout(() => {
            const messagesDiv = document.querySelector('.messages');
            messagesDiv?.scrollTo({ top: messagesDiv.scrollHeight, behavior: 'smooth' });
          });
        });
      }
    });
  }

  trackByFn(index: number, item: any) {
    return item.timeSent;
  }

  loadUsers(): void {
    this.signalRService.getUsers().subscribe((data) => {
      this.users = data.filter(user => user.userID !== this.loggedInUserId);
    });
  }

  selectUser(user: GetUserResponse): void {
    this.selectedUserId = user.userID ?? null;
    this.selectedUserName = user.userName ?? '';
    
    if (this.selectedUserId != null) {
      // If we already have messages for this user, show them
      if (this.messageMap[this.selectedUserId]) {
        this.messages = [...this.messageMap[this.selectedUserId]];
      } else {
        this.messages = [];
      }

      this.getMessages(this.loggedInUserId, this.selectedUserId);
    }
  }

  getMessages(senderId: number, receiverId: number): void {
    this.signalRService.getMessageBody(senderId, receiverId)
      .subscribe({
        next: data => {
          const chatPartnerId = receiverId;
          this.messageMap[chatPartnerId] = data;
          this.messages = [...data];
          this.cdr.detectChanges();
        },
        error: err => console.error(err)
      });
  }

  sendMessage(): void {
    if (this.messageBody.trim() && this.selectedUserId !== null) {
      const newMessage = {
        msgSenderId: this.loggedInUserId,
        msgRecieverId: this.selectedUserId,
        body: this.messageBody,
        timeSent: new Date().toISOString()
      };

      if (!this.messageMap[this.selectedUserId]) {
        this.messageMap[this.selectedUserId] = [];
      }

      this.messageMap[this.selectedUserId].push(newMessage);
      this.messages = [...this.messageMap[this.selectedUserId]];

      this.signalRService.sendMessage(
        this.loggedInUserId,
        this.selectedUserId,
        this.messageBody
      );

      this.messageBody = '';
      this.cdr.detectChanges();
    }
  }

  ngOnDestroy(): void {
    this.signalRService.stopConnection();
    this.newMsgSub?.unsubscribe();
  }
}
