import { Component } from '@angular/core';
import { Video } from '../../interfaces/video';
import { VideoService } from '../../services/video/video.service';
import { CategoryService } from '../../services/category/category.service';
import { Category } from '../../interfaces/category';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-video',
  standalone: false,
  
  templateUrl: './video.component.html',
  styleUrl: './video.component.css'
})
export class VideoComponent {
  videoFile!: File;
  fileName = 'No file uploaded yet.';
  uploadProgress = 0;
  isUploading = false;
  uploadSub!: Subscription | null;

  videoData: Video = {
    
    video: new File([], ''),
    videoName: '',
    description: '',
    isFree: true,
    userId: 2, // Placeholder, to be replaced with dynamic userId
    categoryId: 0
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
      this.fileName = this.videoFile.name;
      this.videoData.video = this.videoFile;
    }
  }

  uploadVideo() {
    if  (this.videoData.categoryId === 0) {
      alert('Please select a category before uploading!');
      return;
    }

    if (!this.videoData.videoName.trim()) {
      alert('Please enter a valid name');
      return;
    }

    if (!this.videoFile) {
      alert('Please sleect a video file first!');
      return;
    }

    const formData = new FormData();
    formData.append('file', this.videoFile);
    formData.append('videoName', this.videoData.videoName);
    formData.append('description', this.videoData.description);
    formData.append('isFree', this.videoData.isFree.toString());
    formData.append('userId', this.videoData.userId.toString());
    formData.append('categoryId', this.videoData.categoryId.toString());

    this.isUploading = true;
    this.uploadProgress = 0;

    this.uploadSub = this.videoService.uploadVideo(formData).subscribe({
      next: (event: HttpEvent<any>) => {
        	console.log('Upload Event:', event.type);

        if (event.type === HttpEventType.UploadProgress) {
          this.uploadProgress = event.total
            ? Math.round(100 * (event.loaded / event.total))
            : Math.round(100 * (event.loaded / this.videoFile.size));
          console.log('UP Loaded: ', event.loaded);
          console.log('UP Total: ', event.total);
          console.log('UP File size: ', this.videoFile.size);
          console.log(`UP File is ${this.uploadProgress}% uploaded.`);
        } else if (event.type === HttpEventType.DownloadProgress) {
          this.uploadProgress = event.total
            ? Math.round(100 * (event.loaded / event.total))
            : Math.round(100 * (event.loaded / this.videoFile.size));
          console.log(`DP File is ${this.uploadProgress}% uploaded.`);
        } else if (event.type === HttpEventType.Response) {
          console.log('Upload Successful:', event.body);
          this.isUploading = false;
          this.uploadSub = null;
        }
      },
      error: (error) => {
        console.error('Upload error: ', error);
        this.isUploading = false;
        this.uploadSub = null;
      }
    });
  }

  cancelUpload() {
    if (this.uploadSub) {
      this.uploadSub.unsubscribe();
      this.uploadSub = null;
      this.isUploading = false;
      this.uploadProgress = 0;
      console.log('Upload canceled');
    }
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
