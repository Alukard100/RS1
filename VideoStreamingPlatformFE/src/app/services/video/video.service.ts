import { Injectable } from '@angular/core';
import { APIService } from '../API/api.service';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class VideoService {

  constructor(private apiService: APIService, private http: HttpClient) { }
  
  uploadVideo(videoData: FormData): Observable<any>{
    return this.http.post<any>(`${this.apiService.getApi()}/Video/CreateVideo`, videoData)
  }

  fetchVideo(VideoID: number): Observable<any>{
    return this.http.get<any>(`${this.apiService.getApi()}/Video/GetVideo?id=` + VideoID)
  }
}
