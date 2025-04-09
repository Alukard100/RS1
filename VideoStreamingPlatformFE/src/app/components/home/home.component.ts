import { Component } from '@angular/core';
import { VideoService } from '../../services/video/video.service';
import { VideoListItem } from '../../interfaces/video-list-item';

@Component({
  selector: 'app-home',
  standalone: false,
  
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {

  constructor(private videoService: VideoService) {}

  videos: VideoListItem[] = [];

  ngOnInit() {
    this.getVideos();
  }

  getVideos() {
    this.videoService.fetchVideos(0).subscribe({
      next: (response) => {
        this.videos = response.map((video: any) => ({
          VideoId: video.videoId,
          VideoName: video.videoName,
          CategoryId: video.categoryId,
          UserName: video.userName,
          ClickCounter: video.clickCounter,
          ThumbnailPicture: video.thumbnailPicture ? 'data:image/jpeg;base64,' + video.thumbnailPicture : '',
          DurationInSeconds: video.durationInSeconds,
          UploadDate: video.uploadDate,
          CategoryName: video.categoryName
          
        }));
        console.log("Fetch success: ", this.videos);
      },
      error: (error) => console.log('Fetch Failure: ', error)
    });
  }
}
