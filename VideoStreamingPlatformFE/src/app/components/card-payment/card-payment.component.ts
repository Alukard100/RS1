import { Component, OnInit, OnDestroy } from '@angular/core';
import { CardPaymentService } from '../../services/Payment/card-payment.service';
import { Router } from '@angular/router';
import { Stripe, StripeCardElement, loadStripe } from '@stripe/stripe-js';
import { AuthService } from '../../services/Auth/auth.service'; // Import AuthService

@Component({
  selector: 'app-card-payment',
  templateUrl: './card-payment.component.html',
  styleUrls: ['./card-payment.component.css'],
  standalone: false,
})
export class CardPaymentComponent implements OnInit, OnDestroy {
  paymentRequest: any = {
    amount: null,
    email: '',
    phoneNumber: '',
    stripeToken: '', // This will store the token after Stripe Elements generates it
    userId: 0, // Add userId here
  };

  stripe: Stripe | null = null;
  card: StripeCardElement | null = null;
  responseMessage: string = '';

  constructor(
    private cardPaymentService: CardPaymentService,
    private authService: AuthService, // Inject AuthService
    private router: Router
  ) {}

  async ngOnInit(): Promise<void> {
    // Load Stripe.js
    this.stripe = await loadStripe('pk_test_51PMQchKZoDCpubn2bI9WLY1IXjMt4KUV16IvFKRFXAMmXQGdV1XS7DjzdxFNr4lYD8ZDPMcHMfWGfuVEYdxvWSod002MraW1y6'); // Use your Stripe publishable key

    if (this.stripe) {
      const elements = this.stripe.elements();
      this.card = elements.create('card');
      this.card.mount('#card-element');
    }
  }

  ngOnDestroy(): void {
    // Cleanup Stripe Elements when the component is destroyed
    if (this.card) {
      this.card.destroy();
    }
  }

  async submitPayment() {
    if (!this.stripe || !this.card) {
      return;
    }

    const { token, error } = await this.stripe.createToken(this.card);

    if (error) {
      this.responseMessage = `Error: ${error.message}`;
      return;
    }

    // Get userId from AuthService
    const userId = this.authService.getUserId();

    // Check if the userId exists before proceeding
    if (userId === 0) {
      this.responseMessage = 'User ID is invalid or missing';
      return;
    }

    // Add userId to the payment request
    this.paymentRequest.stripeToken = token.id;
    this.paymentRequest.userId = userId; // Add userId here

    // Send the payment data (including the token and userId) to your backend
    this.cardPaymentService.createCardPayment(this.paymentRequest).subscribe(
      (response) => {
        this.responseMessage = 'Payment Successful!';
        this.router.navigate(['/payment-success']);
      },
      (error) => {
        console.error(error); // Log the error for debugging
        this.responseMessage = `Payment Failed: ${error.message}`;
      }
    );
  }
}
