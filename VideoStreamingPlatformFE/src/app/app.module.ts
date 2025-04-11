import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, provideHttpClient, withFetch, withInterceptorsFromDi } from '@angular/common/http';
import { CategoryComponent } from './components/category/category.component';
import { VideoComponent } from './components/video/video.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { VideoViewComponent } from './components/video-view/video-view.component';
import { AuthComponent } from './components/auth/auth.component';
import { NavComponent } from './components/nav/nav.component';
import { MaterialModule } from './material/material.module';
import {CardPaymentComponent} from './components/card-payment/card-payment.component';
import { SupportComponent } from './components/support/support.component';
import { ChatComponent } from './components/chat/chat.component';
import { ChatListComponent } from './components/chat-list/chat-list.component';
import { HomeComponent } from './components/home/home.component';
import { VideoCardComponent } from './components/video-card/video-card.component';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import { RegisterComponent } from './components/register/register.component';
import { CommonModule } from '@angular/common';

@NgModule({
  declarations: [
    AppComponent,
    CategoryComponent,
    VideoComponent,
    VideoViewComponent,
    AuthComponent,
    NavComponent,
    CardPaymentComponent,
    SupportComponent,
    ChatComponent,
    ChatListComponent,
    HomeComponent,
    VideoCardComponent,
    ConfirmDialogComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MaterialModule,
    BrowserAnimationsModule, // VERY important for Angular Material to work properly
    MaterialModule,
    ReactiveFormsModule, CommonModule

  ],
  providers: [
    provideHttpClient(withFetch())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
