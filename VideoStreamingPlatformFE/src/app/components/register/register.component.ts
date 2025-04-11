import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { RegisterService } from '../../services/register/register.service';
import { HttpErrorResponse } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css'],
  standalone: false
})
export class RegisterComponent implements OnInit {
  registerForm: FormGroup;
  isSubmitting = false;
  imagePreview: string | null = null;
  countries: string[] = [
    'United States', 'United Kingdom', 'Canada', 'Australia',
    // Add more countries as needed
  ];

  constructor(private fb: FormBuilder, private registerService: RegisterService, private router: Router) {
    this.registerForm = this.fb.group({
      name: ['', [Validators.required]],
      surname: ['', [Validators.required]],
      userName: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      birthDate: [''],
      country: [''],
      profilePicture: [null]
    });
  }

  ngOnInit() {
    // Remove this in production - it's just for taesting
    this.registerForm.patchValue({
      name: 'Dzile',
      surname: 'Pilavdzic',
      userName: 'dzile123',
      email: 'azerpilavdzic@gmail.com',
      password: 'Test123!',
      birthDate: '1990-01-01',
      country: 'United States'
    });
  }

  onFileSelected(event: any) {
    const file = event.target.files[0];
    if (file) {
      // Update form control
      this.registerForm.patchValue({
        profilePicture: file
      });

      // Create preview
      const reader = new FileReader();
      reader.onload = () => {
        this.imagePreview = reader.result as string;
      };
      reader.readAsDataURL(file);
    }
  }

  register() {
    if (this.registerForm.valid) {
      this.isSubmitting = true;

      this.registerService.register(this.registerForm.value).subscribe({
        next: (response) => {
          console.log('Registration successful', response);
          alert('Registration successful');
          // Handle successful registration (e.g., redirect or show success message)
        },
        error: (error) => {
          console.error('Registration failed', error);
          alert('Registration failed');
          this.isSubmitting = false; // Reset the button state on error
          // Handle error (e.g., show error message to user)
        },
        complete: () => {
          this.isSubmitting = false; // Reset the button state on completion
        }
      });
      this.router.navigate(['/login']);
    } else {
      const errors = [];
      const controls = this.registerForm.controls;

      if (controls['name'].errors?.['required']) {
        errors.push('First name is required');
      }

      if (controls['surname'].errors?.['required']) {
        errors.push('Surname is required');
      }

      if (controls['userName'].errors) {
        if (controls['userName'].errors['required']) {
          errors.push('Username is required');
        } else if (controls['userName'].errors['minlength']) {
          errors.push('Username must be at least 3 characters long');
        }
      }

      if (controls['email'].errors) {
        if (controls['email'].errors['required']) {
          errors.push('Email is required');
        } else if (controls['email'].errors['email']) {
          errors.push('Please enter a valid email address');
        }
      }

      if (controls['password'].errors) {
        if (controls['password'].errors['required']) {
          errors.push('Password is required');
        } else if (controls['password'].errors['minlength']) {
          errors.push('Password must be at least 6 characters long');
        }
      }
      alert(errors.join('\n'));
    }
  }
}
