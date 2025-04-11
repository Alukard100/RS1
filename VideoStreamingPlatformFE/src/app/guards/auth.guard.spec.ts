// auth.guard.spec.ts
import { TestBed } from '@angular/core/testing';
import { Router } from '@angular/router';
import { AuthService } from '../services/Auth/auth.service';
import { AuthGuard } from './auth.guard';

describe('AuthGuard', () => {
  let guard: AuthGuard;
  let authServiceSpy: jasmine.SpyObj<AuthService>;
  let routerSpy: jasmine.SpyObj<Router>;

  beforeEach(() => {
    // create spies for the dependencies
    authServiceSpy = jasmine.createSpyObj('AuthService', ['getLoggedInStatusSync']);
    routerSpy      = jasmine.createSpyObj('Router', ['navigate']);

    TestBed.configureTestingModule({
      providers: [
        AuthGuard,
        { provide: AuthService, useValue: authServiceSpy },
        { provide: Router,      useValue: routerSpy      }
      ]
    });
    guard = TestBed.inject(AuthGuard);
  });

  it('should be created', () => {
    expect(guard).toBeTruthy();
  });

  it('should allow activation when logged in', () => {
    authServiceSpy.getLoggedInStatusSync.and.returnValue(true);
    expect(guard.canActivate()).toBeTrue();
  });

  it('should redirect to /login when not logged in', () => {
    authServiceSpy.getLoggedInStatusSync.and.returnValue(false);
    expect(guard.canActivate()).toBeFalse();
    expect(routerSpy.navigate).toHaveBeenCalledWith(['/login']);
  });
});
