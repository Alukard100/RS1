import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VideoComponent } from './components/video/video.component';
import { CategoryComponent } from './components/category/category.component';
import { AuthComponent } from './components/auth/auth.component';

const routes: Routes = [
  // { path: 'Videos', component: VideoComponent },
  // { path: 'Category', component: CategoryComponent }

  { path: '', redirectTo: 'login', pathMatch: 'full' },

  { path: 'login', component: AuthComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
