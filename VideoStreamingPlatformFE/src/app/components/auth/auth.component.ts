import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from '../../services/Auth/auth.service';
import { LoginRequest } from '../../interfaces/Auth/login-request.interface';

@Component({
  selector: 'app-auth',
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css',
  standalone:false
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
        //After successful login we will navigate to home
        this.router.navigate(['/home']);
      },
      error: (error) => {
        this.errorMessage = 'Invalid credentials. Try again.';
      }
    });
  }
}
