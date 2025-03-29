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

@NgModule({
  declarations: [
    AppComponent,
    CategoryComponent,
    VideoComponent,
    VideoViewComponent,
    AuthComponent,
    NavComponent,
    CardPaymentComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    MaterialModule
  ],
  providers: [
    provideHttpClient(withFetch())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
