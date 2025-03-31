import { Component, OnInit } from '@angular/core';
import { SupportService } from '../../services/Support/support.service';
import {AuthService} from '../../services/Auth/auth.service';

@Component({
  selector: 'app-support',
  templateUrl: './support.component.html',
  styleUrls: ['./support.component.css'],
  standalone:false
})
export class SupportComponent implements OnInit {
  supportMessages: any[] = [];
  newMessage: string = '';
  userId: number = 0;

  constructor(private supportService :SupportService,private authService: AuthService) {}

  ngOnInit(): void {
    this.userId=this.authService.getUserId();
    this.loadSupportMessages();
  }

  loadSupportMessages(): void {
    this.supportService.getSupport(this.userId).subscribe(
      (data) => {
        this.supportMessages = data;
      },
      (error) => {
        console.error('Error loading support messages:', error);
      }
    );
  }

  sendMessage(): void {
    if (!this.newMessage.trim()) return;

    this.supportService.createSupport(this.newMessage, this.userId).subscribe(
      () => {
        this.newMessage = '';
        this.loadSupportMessages();
      },
      (error) => {
        console.error('Error sending support message:', error);
      }
    );
  }

  deleteMessage(supportId: number): void {
    this.supportService.deleteSupport(supportId).subscribe(
      () => {
        this.supportMessages = this.supportMessages.filter(msg => msg.SupportId !== supportId);
      },
      (error) => {
        console.error('Error deleting support message:', error);
      }
    );
  }
}
