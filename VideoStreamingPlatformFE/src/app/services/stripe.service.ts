import { Injectable } from '@angular/core';
import { loadStripe, Stripe, StripeCardElement } from '@stripe/stripe-js';

@Injectable({
  providedIn: 'root'
})
export class StripeService {
  stripe: Stripe | null = null;
  card: StripeCardElement | null = null;

  constructor() {}

  async initializeStripe(): Promise<void> {
    // Load Stripe.js library
    this.stripe = await loadStripe('pk_test_51PMQchKZoDCpubn2bI9WLY1IXjMt4KUV16IvFKRFXAMmXQGdV1XS7DjzdxFNr4lYD8ZDPMcHMfWGfuVEYdxvWSod002MraW1y6'); // Replace with your Stripe publishable key

    if (this.stripe) {
      const elements = this.stripe.elements();
      this.card = elements.create('card');
    }
  }

  getStripeCardElement(): StripeCardElement | null {
    return this.card;
  }

  async createToken(card: StripeCardElement): Promise<{ token: string | null, error: string | null }> {
    if (!this.stripe || !card) {
      return { token: null, error: 'Stripe not initialized or card element not available.' };
    }

    const { token, error } = await this.stripe.createToken(card);

    if (error) {
      return { token: null, error: error.message || '' }; // Ensure error is always a string (fallback to empty string)
    }

    return { token: token?.id ?? null, error: null };
  }

}
