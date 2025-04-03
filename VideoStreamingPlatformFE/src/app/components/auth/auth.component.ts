import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/Auth/auth.service';
import { LoginRequest } from '../../interfaces/Auth/login-request.interface';
import { VerifyCodeRequest } from '../../interfaces/Auth/verify-code-request.interface';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
  standalone: false
})
export class AuthComponent {
  email: string = '';
  password: string = '';
  verificationCode: string = '';
  errorMessage: string = '';
  showVerificationModal: boolean = false;  // Flag to show/hide modal

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {
    const request: LoginRequest = { email: this.email, password: this.password };

    this.authService.login(request).subscribe({
      next: (response) => {
        this.authService.setSession(response);
        console.log('Logged in as:', this.authService.getUserName());

        // Show the verification modal after successful login
        this.showVerificationModal = true;
      },
      error: (err) => {
        console.error('Login failed:', err);
        this.errorMessage = 'Login failed, please try again.';
      }
    });
  }

  onVerifyCode() {
    const userId = this.authService.getUserId();
    const request: VerifyCodeRequest = { userId, code: this.verificationCode };

    this.authService.verifyCode(request).subscribe({
      next: (response) => {
        if (response && response.Token) {
          // Set the session and navigate to another page
          this.authService.setSession(response);
          this.router.navigate(['/card-payment']);
        } else {
          this.errorMessage = 'Invalid verification code.';
        }
      },
      error: (err) => {
        console.error('Verification failed:', err);
        this.errorMessage = 'Verification failed, please try again.';
      }
    });

    // Close the modal after submitting
    this.showVerificationModal = false;
  }

  closeModal() {
    this.showVerificationModal = false;
  }
}
