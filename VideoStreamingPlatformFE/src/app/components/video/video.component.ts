import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { Video } from '../../interfaces/video';
import { VideoService } from '../../services/video/video.service';
import { CategoryService } from '../../services/category/category.service';
import { Category } from '../../interfaces/category';
import { HttpEvent, HttpEventType } from '@angular/common/http';
import { Subscription } from 'rxjs';
import { create } from 'node:domain';

@Component({
  selector: 'app-video',
  standalone: false,
  
  templateUrl: './video.component.html',
  styleUrl: './video.component.css'
})
export class VideoComponent implements OnInit {
  videoFile!: File;
  fileName = 'Chose your file here';
  uploadProgress = 0;
  isUploading = false;
  uploadSub!: Subscription | null;
  @Output() public onUploadFinished = new EventEmitter();
  isDragging = false;

  

  videoData: Video = {
    
    filePath: '',
    RealFilePath: '',
    videoName: '',
    description: ' ',
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
      const file = input.files[0];

      if (this.validateVideoFile(file)) {
        this.videoFile = input.files[0];
        this.fileName = this.videoFile.name;
      } else {
        alert('Only video files (.mp4, .mov, .avi) are allowed.');
      }
      
    }
  }

  onDragOver(event: DragEvent) {
    event.preventDefault();
    this.isDragging = true;
  }

  onDragLeave(event: DragEvent) {
    event.preventDefault();
    this.isDragging = false;
  }

  onDrop(event: DragEvent) {
    event.preventDefault();
    this.isDragging = false;

    const droppedFiles = event.dataTransfer?.files;
    if (droppedFiles && droppedFiles.length > 0) {
      const file = droppedFiles[0];

      if (this.validateVideoFile(file)) {
        this.videoFile = file;
        this.fileName = file.name;
      } else {
        alert('Only video files (.mp4, .mov, .avi) are allowed.');
      }
    }
  }

  validateVideoFile(file: File): boolean {
    const allowedExtensions = ['.mp4', '.mov', '.avi'];
    const extension = file.name.slice(file.name.lastIndexOf('.')).toLowerCase();
    return allowedExtensions.includes(extension);
  }

  createVideo() {
    if (!this.videoFile) {
      alert('Please select a video file first!');
      return;
    }

    if  (this.videoData.categoryId === 0) {
      alert('Please select a category before uploading!');
      return;
    }

    if (!this.videoData.videoName.trim()) {
      alert('Please enter a valid name');
      return;
    }

    const formData = new FormData();
    formData.append('file', this.videoFile);

    this.isUploading = true;
    this.uploadProgress = 0;

    //Uplad the video file
    this.uploadSub = this.videoService.uploadVideoFile(formData).subscribe({
      next: (event) => {
        if (event.type === HttpEventType.UploadProgress) {
          this.uploadProgress = event.total
            ? Math.round(100 * (event.loaded / event.total))
            : Math.round(100 * (event.loaded / this.videoFile.size));
        } else if (event.type === HttpEventType.Response) {
          //get my file path
          this.videoData.filePath = event.body.videoUrl;
          this.videoData.RealFilePath = event.body.filePath;

          if (!this.videoData.filePath || this.videoData.filePath === '0') {
            alert('File upload failed or returned an invalid response.');
            this.isUploading = false;
            return;
          }

          const videoDataForm = new FormData();
          videoDataForm.append('filePath', this.videoData.filePath);
          videoDataForm.append('RealFilePath', this.videoData.RealFilePath);
          videoDataForm.append('videoName', this.videoData.videoName);
          videoDataForm.append('description', this.videoData.description);
          videoDataForm.append('isFree', this.videoData.isFree.toString());
          videoDataForm.append('userId', this.videoData.userId.toString());
          videoDataForm.append('categoryId', this.videoData.categoryId.toString());

          this.uploadSub = this.videoService.uploadVideo(videoDataForm).subscribe({
            next: (createEvent) => {
              if (createEvent.type === HttpEventType.UploadProgress) {
                this.uploadProgress = createEvent.total
                  ? Math.round(100 * (createEvent.loaded / createEvent.total))
                  : Math.round(100 * (createEvent.loaded / this.videoFile.size));
              } else if (createEvent.type === HttpEventType.Response) {
                console.log('Video creation succeessful: ', createEvent.body);
                this.onUploadFinished.emit(createEvent.body);

                setTimeout(() => {
                  this.isUploading = false;
                  this.uploadProgress = 0;
                }, 2000);
              }
            },
            error: (err) => {
              console.error('Video creation error: ', err);
              this.isUploading = false;
            }
          });
        }
      },
      error: (error) => {
        console.error('File upload error', error);
        this.isUploading = false;
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
