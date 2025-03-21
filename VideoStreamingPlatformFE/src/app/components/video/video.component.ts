import { Component } from '@angular/core';
import { Video } from '../../interfaces/video';
import { VideoService } from '../../services/video/video.service';
import { CategoryService } from '../../services/category/category.service';
import { response } from 'express';
import { error } from 'console';
import { Category } from '../../interfaces/category';

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
    userId: 2, // Placeholder, to be replaced with dynamic userId
    categoryId: 1 // 0 Placeholder, implemented category select
  };

  categories: Category[] = [];
  
  constructor(private videoService: VideoService, private categoryService: CategoryService) {}

  ngOnInit() {
    this.getCategories();
  }

  onFileSelected(event: Event) {
    const input = event.target as HTMLInputElement;
    if (input.files && input.files.length > 0) {
      this.videoFile = input.files[0];
      this.videoData.video = this.videoFile;
    }
  }

  uploadVideo() {
    if  (this.videoData.categoryId === 0) {
      alert('Please select a category before uploading!');
      return;
    }

    const formData = new FormData();
    formData.append('file', this.videoFile);
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

  getCategories() {
    this.categoryService.fetchCategory(0).subscribe({
      next: (response) => {
        this.categories = response;
        console.log("Fetch success: ", this.categories);
      },
      error: (error) => console.log('Fetch failure: ', error)
    });
  }
  
}
