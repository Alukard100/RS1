import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VideoComponent } from './components/video/video.component';
import { CategoryComponent } from './components/category/category.component';
import { VideoViewComponent } from './components/video-view/video-view.component';
import { AuthComponent } from './components/auth/auth.component';
import { SettingsComponent } from './components/settings/settings.component';
import {CardPaymentComponent} from './components/card-payment/card-payment.component';
import {SupportComponent} from './components/support/support.component';
import {ChatComponent} from './components/chat/chat.component';
import {ChatListComponent} from './components/chat-list/chat-list.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './guards/auth.guard';
import { RegisterComponent } from './components/register/register.component';
import {AdminPanelComponent} from './components/admin-panel/admin-panel.component';
import {AdminGuard} from './guards/admin.guard';


const routes: Routes = [
  { path: 'CreateVideo', component: VideoComponent, canActivate: [AuthGuard] },
  { path: 'settings', component: SettingsComponent, canActivate: [AuthGuard] },
    { path: 'card-payment', component: CardPaymentComponent, canActivate: [AuthGuard] },
    { path: 'register', component: RegisterComponent },
    { path: 'support', component: SupportComponent, canActivate:[AuthGuard] },
  { path: 'chat-list', component: ChatListComponent, canActivate: [AuthGuard] },
  { path: 'chat', component: ChatComponent, canActivate: [AuthGuard] },
  { path: 'chat/:userId', component: ChatComponent, canActivate: [AuthGuard] },
  { path: 'home', component: HomeComponent },
  { path: 'video/:id', component: VideoViewComponent },
  { path: '', redirectTo: 'home', pathMatch: 'full' },
  { path: 'login', component: AuthComponent },
  {path: 'admin-panel', component: AdminPanelComponent, canActivate: [AdminGuard] },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
