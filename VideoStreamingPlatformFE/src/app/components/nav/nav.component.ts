import { isPlatformBrowser } from '@angular/common';
import { Component, Inject, OnInit, PLATFORM_ID } from '@angular/core';
import { AuthService } from '../../services/Auth/auth.service';
import { CardPaymentService } from '../../services/Payment/card-payment.service';
import {Router} from '@angular/router';


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
  walletBalance:number|null=null;
  isChatOpen=false;
  isAdmin:boolean=false;

  constructor(
    private authService: AuthService,
    private cardPaymentService: CardPaymentService,
    private router:Router,
    @Inject(PLATFORM_ID) private platformId: Object
  ) {
    this.isBrowser = isPlatformBrowser(this.platformId);
  }

  ngOnInit(): void {
    if (this.isBrowser) {
      this.authService.getLoggedInStatus().subscribe(status => {
        this.isLoggedIn = status;
        this.isAdmin=this.authService.isAdmin();
        if (status) {
          // ← set the name as soon as we know we’re logged in
          this.userName = this.authService.getUserName();

          const userId = this.authService.getUserId();
          this.cardPaymentService.getWallet(userId).subscribe({
            next: resp => this.walletBalance = resp.balance,
            error: err => console.error('Failed to load wallet:', err)
          });
        } else {
          this.userName = '';
          this.walletBalance = null;
        }
      });
    }
  }


  logout(): void {
    this.authService.logout();
  }

  goToCardPayment() {
    this.router.navigate(['/card-payment'])
  }

  toggleChat() {
    this.isChatOpen=!this.isChatOpen;
  }
}
