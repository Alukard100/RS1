import { Injectable } from '@angular/core';
import { APIService } from '../API/api.service';
import { HttpClient, HttpEvent, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VideoService {

  constructor(private apiService: APIService, private http: HttpClient) { }
  
  uploadVideoFile(videoData: FormData): Observable<any>{
    return this.http.post(`${this.apiService.getApi()}/Video/UploadVideoFile`, videoData, {
      headers: new HttpHeaders(),
      reportProgress: true,
      observe: 'events'
    })
  }

  uploadVideo(videoData: FormData): Observable<HttpEvent<any>>{
    return this.http.post<any>(`${this.apiService.getApi()}/Video/CreateVideo`, videoData, {
      headers: new HttpHeaders(),
      reportProgress: true,
      observe: 'events'
    })
  }

  fetchVideo(VideoID: number): Observable<any>{
    return this.http.get<any>(`${this.apiService.getApi()}/Video/GetVideo?id=` + VideoID)
  }

  getVideoFile(VideoID: number): Observable<Blob> {
    return this.http.get(`${this.apiService.getApi()}/Video/stream/${VideoID}`, { responseType: 'blob' })
  }
}
