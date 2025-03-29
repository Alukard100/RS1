import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VideoComponent } from './components/video/video.component';
import { CategoryComponent } from './components/category/category.component';
import { VideoViewComponent } from './components/video-view/video-view.component';
import { AuthComponent } from './components/auth/auth.component';
import { SettingsComponent } from './components/settings/settings.component';
import {CardPaymentComponent} from './components/card-payment/card-payment.component';
import {SupportComponent} from './components/support/support.component';

const routes: Routes = [
  { path: 'CreateVideo', component: VideoComponent }, //sets /video to show Video Component
  { path: 'video/:id', component: VideoViewComponent},

  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'login', component: AuthComponent },
  { path: 'settings', component: SettingsComponent },
  { path: 'card-payment', component: CardPaymentComponent },
  { path: 'support', component: SupportComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
