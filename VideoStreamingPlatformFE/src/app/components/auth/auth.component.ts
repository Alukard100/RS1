import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/Auth/auth.service';
import { LoginRequest } from '../../interfaces/Auth/login-request.interface';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrls: ['./auth.component.css'],
  standalone: false
})
export class AuthComponent {
  email: string = '';
  password: string = '';
  errorMessage: string = '';

  constructor(private authService: AuthService, private router: Router) {}

  onLogin() {
    const request: LoginRequest = { email: this.email, password: this.password };

    this.authService.login(request).subscribe({
      next: (response) => {
        this.authService.setSession(response);
        console.log('Logged in as:', this.authService.getUserName());
        this.router.navigate(['/card-payment']);
      },
      error: (err) => {
        console.error('Login failed:', err);  // This will help you catch any issues
      }
    });

  }
}
