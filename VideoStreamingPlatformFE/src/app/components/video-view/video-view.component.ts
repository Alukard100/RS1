import { Component, OnInit } from '@angular/core';
import { VideoService } from '../../services/video/video.service';
import { ActivatedRoute } from '@angular/router';
import { VideoResponse } from '../../interfaces/video-response';

@Component({
  selector: 'app-video-view',
  standalone: false,
  
  templateUrl: './video-view.component.html',
  styleUrl: './video-view.component.css'
})
export class VideoViewComponent implements OnInit{

  videoData: VideoResponse = {
    VideoId: 0,
    VideoName: '',
    Description: '',
    UploadDate: new Date,
    CategoryName: '',
    UserName: '',
    ClickCounter: 0
  }
  videoUrl: any;

  constructor(private videoService: VideoService, private route: ActivatedRoute) {
    this.getAll();
  }

  ngOnInit() {
    this.getAll();
  }

  getAll() {
    this.route.paramMap.subscribe(params => {
      const id = params.get('id');
      if (id) {
        this.videoData.VideoId = +id;
        this.getVideo(this.videoData.VideoId);
        this.getVideoFile(this.videoData.VideoId);
      }
    });
  }

  getVideoFile(videoId: number) {
    this.videoService.getVideoFile(videoId).subscribe({
      next: (blob) => {
        const objectUrl = URL.createObjectURL(blob);
        this.videoUrl = objectUrl;
      },
      error: (error) => console.error('Error fetching video: ', error)
    });
  }

  getVideo(VideoID: number) {
    this.videoService.fetchVideo(VideoID).subscribe({
      next: (response) => {
        const video = response[0];

        this.videoData.VideoName = video.videoName;
        this.videoData.Description = video.description;
        this.videoData.UploadDate = video.uploadDate;
        this.videoData.CategoryName = video.categoryName;
        this.videoData.UserName = video.userName;
        this.videoData.ClickCounter = video.clickCounter;

        console.log(this.videoData);

      },
      error: (error) => console.error('Get error: ', error)
    });
  }

}
