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
    showVerificationModal: boolean = false;

    constructor(private authService: AuthService, private router: Router) {}

    onLogin() {
      const request: LoginRequest = { email: this.email, password: this.password };

      this.authService.login(request).subscribe({
        next: (response) => {
          console.log('Login response:', response);

          // Use the correct casing based on the JSON keys
          localStorage.setItem('typeId', response.typeId?.toString() ?? '');
          localStorage.setItem('userId', response.userId?.toString() ?? '');
          localStorage.setItem('userName', response.userName ?? '');

          console.log('Logged in as:', this.authService.getUserName());

          this.showVerificationModal = true;
        },
        error: (err) => {
          console.error('Login failed:', err);
          this.errorMessage = 'Login failed, please try again.';
          localStorage.clear();
        }
      });
    }


    onVerifyCode() {
      const UserId = this.authService.getUserId();
      const request: VerifyCodeRequest = { UserId, Code: this.verificationCode };

      this.authService.verifyCode(request).subscribe({
        next: (response) => {
          console.log('Verification response:', response);

          if (response && response.token) {
            this.authService.setSession(response);
            this.router.navigate(['/home']);
          } else {
            this.errorMessage = 'Invalid verification code.';
            localStorage.clear();
          }
        },
        error: (err) => {
          console.error('Verification failed:', err);
          this.errorMessage = 'Verification failed, please try again.';

          localStorage.clear();
        }
      });
    }


    closeModal() {
      this.showVerificationModal = false;
    }
  }
