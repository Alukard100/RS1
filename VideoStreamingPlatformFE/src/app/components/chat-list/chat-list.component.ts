import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../../services/Chat/signal-r.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  standalone:false,
  styleUrls: ['./chat-list.component.css'],
})
export class ChatListComponent implements OnInit {
  users: any[] = [];
  selectedUserId: number | null = null;

  constructor(private signalRService: SignalRService, private router: Router) {}

  selectUser(userId: number): void {
    this.router.navigate(['/chat', userId]);  // Navigate to ChatComponent with selected userId
  }

  ngOnInit(): void {
    // Fetch users when the component loads
    this.getUsers();
  }

  // Fetch users
  getUsers(): void {
    this.signalRService.getUsers().subscribe(users => {
      this.users = users;
    });
  }
}
