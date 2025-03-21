import { NgModule } from '@angular/core';
import { BrowserModule, provideClientHydration, withEventReplay } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HttpClientModule, provideHttpClient, withFetch, withInterceptorsFromDi } from '@angular/common/http';
import { CategoryComponent } from './components/category/category.component';
import { VideoComponent } from './components/video/video.component';
import { FormsModule } from '@angular/forms';
import { AuthComponent } from './components/auth/auth.component'

@NgModule({
  declarations: [
    AppComponent,
    CategoryComponent,
    VideoComponent,
    AuthComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    provideHttpClient(withFetch())
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
