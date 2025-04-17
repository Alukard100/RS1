import { Component, OnInit, OnDestroy } from '@angular/core';
import { SignalRService } from '../../services/Chat/signal-r.service';
import { GetUserResponse } from '../../interfaces/get-user-response.model';
import {Subscription} from 'rxjs';

@Component({
  selector: 'app-chat',
  templateUrl: './chat.component.html',
  styleUrls: ['./chat.component.css'],
  standalone: false,
})
export class ChatComponent implements OnInit, OnDestroy {
  users: any[] = [];
  messages: any[] = [];
  selectedUserId: number | null = null;
  selectedUserName: string = '';
  messageBody: string = '';
  loggedInUserId: number = 0;
  private newMsgSub?: Subscription;

  constructor(private signalRService: SignalRService) {}

  ngOnInit(): void {
    const userId = localStorage.getItem('userId');
    if (userId) {
      this.loggedInUserId = +userId;
      this.signalRService.startConnection(this.loggedInUserId);
    }

    this.loadUsers();

    // Subscribe to the message stream to update the UI
    this.signalRService.messages$.subscribe((msgs) => {
      if (this.selectedUserId !== null) {
        // Filter messages by the selected user (either sender or receiver)
        this.messages = msgs.filter(
          msg =>
            (msg.msgSenderId === this.loggedInUserId && msg.msgRecieverId === this.selectedUserId) ||
            (msg.msgSenderId === this.selectedUserId && msg.msgRecieverId === this.loggedInUserId)
        );
      }
    });
    this.newMsgSub = this.signalRService.newMessage$
      .subscribe((msg: any) => {
        const isForThisChat = (
          (msg.msgSenderId === this.loggedInUserId && msg.msgRecieverId === this.selectedUserId) ||
          (msg.msgSenderId === this.selectedUserId   && msg.msgRecieverId === this.loggedInUserId)
        );
        if (isForThisChat) {
          this.messages.push(msg);
        }
      });
  }

  loadUsers(): void {
    this.signalRService.getUsers().subscribe((data) => {
      // Filter out the logged-in user from the users list
      this.users = data.filter(user => user.userID !== this.loggedInUserId);
    });
  }

  selectUser(user: GetUserResponse) {
    // clear out old messages array and load fresh history
    this.selectedUserId = user.userID ?? null;
    this.selectedUserName = user.userName ?? '';
    this.messages = [];
    if (this.selectedUserId != null) {
      this.getMessages(this.loggedInUserId, this.selectedUserId);
    }
  }

  getMessages(senderId: number, receiverId: number) {
    this.signalRService.getMessageBody(senderId, receiverId)
      .subscribe({
        next: data => {
          this.messages = data;  // initial load
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

      // Immediately add the message to the local message list for the sender
      this.messages.push(newMessage);

      // Send the message via SignalR
      this.signalRService.sendMessage(
        this.loggedInUserId,
        this.selectedUserId,
        this.messageBody
      );

      this.messageBody = ''; // Clear the message input
    }
  }

  ngOnDestroy() {
    this.signalRService.stopConnection();
    this.newMsgSub?.unsubscribe();
  }
}
