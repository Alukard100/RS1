import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../../services/Chat/signal-r.service';
import { Router } from '@angular/router';
import { AuthService } from '../../services/Auth/auth.service';

@Component({
  selector: 'app-chat-list',
  templateUrl: './chat-list.component.html',
  standalone:false,
  styleUrls: ['./chat-list.component.css'],
})
export class ChatListComponent implements OnInit {
  users: any[] = [];
  selectedUserId: number | null = null;
  loggedInUserId: number = 0;

  constructor(
    private signalRService: SignalRService,
    private router: Router,
    private authService: AuthService
  ) {}

  selectUser(userId: number): void {
    console.log('Selected user:', userId);
    this.selectedUserId = userId;
    this.router.navigate(['/chat', userId]);
  }

  ngOnInit(): void {
    // Get the logged-in user's ID
    this.loggedInUserId = this.authService.getUserId();
    
    // Fetch users when the component loads
    this.getUsers();
  }

  // Fetch users
  getUsers(): void {
    this.signalRService.getUsers().subscribe({
      next: (users) => {
        console.log('Fetched users:', users);
        // Filter out the current user from the list
        this.users = users.filter((user: any) => user.userId !== this.loggedInUserId);
      },
      error: (error) => {
        console.error('Error fetching users:', error);
      }
    });
  }
}
