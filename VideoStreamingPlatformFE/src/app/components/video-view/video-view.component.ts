import { Component, OnInit } from '@angular/core';
import { VideoService } from '../../services/video/video.service';
import { ActivatedRoute } from '@angular/router';
import { VideoResponse } from '../../interfaces/video-response';
import { response } from 'express';
import { error } from 'console';
import { blob } from 'stream/consumers';

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
    FilePath: '',
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
        this.videoData.VideoName = response.videoName;
        this.videoData.Description = response.description;
        this.videoData.FilePath = response.filePath;
        this.videoData.UploadDate = response.uploadDate;
        this.videoData.CategoryName = response.categoryNmae;
        this.videoData.UserName = response.userName;
        this.videoData.ClickCounter = response.clickCounter;
        console.log(this.videoData);

      },
      error: (error) => console.error('Get error: ', error)
    });
  }

}
