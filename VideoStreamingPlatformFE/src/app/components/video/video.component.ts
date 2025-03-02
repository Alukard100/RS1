import { Component } from '@angular/core';
import { Video } from '../../interfaces/video';
import { VideoService } from '../../services/video/video.service';

@Component({
  selector: 'app-video',
  standalone: false,
  
  templateUrl: './video.component.html',
  styleUrl: './video.component.css'
})
export class VideoComponent {
  videoFile!: File;
  
  videoData: Video = {
    
    video: new File([], ''),
    videoName: '',
    description: '',
    isFree: true,
    userId: 3, // Placeholder, to be replaced with dynamic userId
    categoryId: 0 // 0 Placeholder, implemented category select
  };
  
  constructor(private videoService: VideoService) {}

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.videoFile = input.files[0];
      this.videoData.video = this.videoFile;
    }
  }

  uploadVideo() {
    const formData = new FormData();
    formData.append('video', this.videoFile);
    formData.append('videoName', this.videoData.videoName);
    formData.append('description', this.videoData.description);
    formData.append('isFree', this.videoData.isFree.toString());
    formData.append('userId', this.videoData.userId.toString());
    formData.append('categoryId', this.videoData.categoryId.toString());

    this.videoService.uploadVideo(formData).subscribe({
      next: (response) => console.log('Upload Successful: ', response),
      error: (error) => console.error('Upload error: ', error)
    });
  }
  

}
