import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, provideHttpClient, withFetch, withInterceptorsFromDi } from '@angular/common/http';
import { CategoryComponent } from './components/category/category.component';
import { VideoComponent } from './components/video/video.component';
import { FormsModule } from '@angular/forms';
import { VideoViewComponent } from './components/video-view/video-view.component';
import { AuthComponent } from './components/auth/auth.component';
import { NavComponent } from './components/nav/nav.component';
import { MaterialModule } from './material/material.module';
import {CardPaymentComponent} from './components/card-payment/card-payment.component';
import { SupportComponent } from './components/support/support.component';
import { ChatComponent } from './components/chat/chat.component';
import { ChatListComponent } from './components/chat-list/chat-list.component';
import { MatDialogModule } from '@angular/material/dialog';
import { ConfirmDialogComponent } from './confirm-dialog/confirm-dialog.component';
import {MatSidenav, MatSidenavContainer} from "@angular/material/sidenav";
import { MatSidenavModule } from '@angular/material/sidenav';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';

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
    ConfirmDialogComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MaterialModule,
    MatDialogModule,
    MatSidenavContainer,
    MatSidenav,
    BrowserAnimationsModule, // VERY important for Angular Material to work properly
    MatSidenavModule,
    MatToolbarModule,
    MatIconModule,
    MatListModule,
    MatButtonModule,
  ],
  providers: [
    provideHttpClient(withFetch())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
