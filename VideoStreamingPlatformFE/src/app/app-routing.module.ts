import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VideoComponent } from './components/video/video.component';
import { CategoryComponent } from './components/category/category.component';

const routes: Routes = [
  { path: 'CreateVideo', component: VideoComponent }, //sets /video to show Video Component 
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
