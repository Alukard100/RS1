import { Component, OnInit, OnDestroy } from '@angular/core';
import { CardPaymentService } from '../../services/Payment/card-payment.service';
import { Router } from '@angular/router';
import { Stripe, StripeCardElement, loadStripe } from '@stripe/stripe-js';
import { AuthService } from '../../services/Auth/auth.service';

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
    stripeToken: '',
    userId: 0,
  };

  promoCode: string = '';
  stripe: Stripe | null = null;
  card: StripeCardElement | null = null;
  responseMessage: string = '';
  responseMessageType: 'success' | 'error' = 'success'; // For response message styling

  constructor(
    private cardPaymentService: CardPaymentService,
    private authService: AuthService,
    private router: Router
  ) {}

  async ngOnInit(): Promise<void> {
    // Load Stripe.js
    this.stripe = await loadStripe('pk_test_51PMQchKZoDCpubn2bI9WLY1IXjMt4KUV16IvFKRFXAMmXQGdV1XS7DjzdxFNr4lYD8ZDPMcHMfWGfuVEYdxvWSod002MraW1y6');

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
      this.responseMessage = 'Stripe is not properly loaded.';
      this.responseMessageType = 'error';
      return;
    }

    const { token, error } = await this.stripe.createToken(this.card);

    if (error) {
      this.responseMessage = `Error: ${error.message}`;
      this.responseMessageType = 'error';
      return;
    }

    // Get userId from AuthService
    const userId = this.authService.getUserId();

    if (userId === 0) {
      this.responseMessage = 'User ID is invalid or missing';
      this.responseMessageType = 'error';
      return;
    }

    // Add userId to the payment request
    this.paymentRequest.stripeToken = token.id;
    this.paymentRequest.userId = userId;

    this.cardPaymentService.createCardPayment(this.paymentRequest).subscribe(
      (response) => {
        this.responseMessage = 'Payment Successful!';
        this.responseMessageType = 'success';
        this.router.navigate(['/payment-success']);
      },
      (error) => {
        console.error(error);
        this.responseMessage = `Payment Failed: ${error.message}`;
        this.responseMessageType = 'error';
      }
    );
  }

  activatePromoCode() {
    const userId = this.authService.getUserId();

    if (!userId || userId <= 0) {
      this.responseMessage = 'User ID is missing or invalid.';
      this.responseMessageType = 'error';
      return;
    }

    if (!this.promoCode || this.promoCode.trim() === '') {
      this.responseMessage = 'Please enter a promo code.';
      this.responseMessageType = 'error';
      return;
    }

    const request = {
      userId: userId,
      codeValue: this.promoCode.trim(), // Trim extra spaces
    };

    console.log('Sending request:', request);

    this.cardPaymentService.enterPromoCode(request).subscribe(
      (response) => {
        console.log('Promo Code Response:', response);
        this.responseMessage = response.message;
        this.responseMessageType = 'success';
      },
      (error) => {
        console.error('Promo Code Error:', error);
        this.responseMessage = `Promo Code Activation Failed: ${error.error}`;
        this.responseMessageType = 'error';
      }
    );
  }
}
