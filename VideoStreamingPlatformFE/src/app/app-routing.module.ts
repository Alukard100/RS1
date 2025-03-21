import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { VideoComponent } from './components/video/video.component';
import { CategoryComponent } from './components/category/category.component';
import { VideoViewComponent } from './components/video-view/video-view.component';

const routes: Routes = [
  { path: 'CreateVideo', component: VideoComponent }, //sets /video to show Video Component 
  { path: 'video/:id', component: VideoViewComponent},
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
