import { isPlatformBrowser } from '@angular/common';
import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { AuthService } from '../../services/Auth/auth.service'; // adjust path as needed

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css',
  standalone: false
})
export class NavComponent implements OnInit {
  isBrowser: boolean = false;
  isLoggedIn: boolean = false;
  userName: string = '';

  constructor(
    private authService: AuthService,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(this.platformId);
  }

  ngOnInit(): void {
    if (this.isBrowser) {
      this.authService.getLoggedInStatus().subscribe((status) => {
        this.isLoggedIn = status;
        this.userName = this.authService.getUserName();
      });
    }
  }

  logout(): void {
    this.authService.logout();
  }
}
