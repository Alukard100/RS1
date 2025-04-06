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
  fileName = 'No file uploaded yet.';
  uploadProgress = 0;
  isUploading = false;
  uploadSub!: Subscription | null;
  @Output() public onUploadFinished = new EventEmitter();

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
      this.videoFile = input.files[0];
      this.fileName = this.videoFile.name;
    }
  }

  uploadFile = (file: File) => {
    if (!file) {
      alert('Please slelect a video file first!');
      return;
    }

    const formData = new FormData();
    formData.append('file', file);

    this.videoService.uploadVideoFile(formData).subscribe(event => 
    {
      console.log(event.type);
      if(event.type === HttpEventType.UploadProgress) {
        this.isUploading = true;
        this.uploadProgress = event.total
            ? Math.round(100 * (event.loaded / event.total))
            : Math.round(100 * (event.loaded / this.videoFile.size));
      } else if (event.type === HttpEventType.DownloadProgress) {
        this.isUploading = true;
        this.uploadProgress = event.total
            ? Math.round(100 * (event.loaded / event.total))
            : Math.round(100 * (event.loaded / this.videoFile.size));
      }
      else if(event.type === HttpEventType.Response) {
        this.onUploadFinished.emit(event.body);
      }
    });
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
