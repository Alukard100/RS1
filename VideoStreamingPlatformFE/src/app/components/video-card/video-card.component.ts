import { Component, Input } from '@angular/core';
import { VideoListItem } from '../../interfaces/video-list-item';
import { Router } from '@angular/router';

@Component({
  selector: 'app-video-card',
  standalone: false,
  
  templateUrl: './video-card.component.html',
  styleUrl: './video-card.component.css'
})



export class VideoCardComponent {
  @Input() video!: VideoListItem;

  constructor(private router: Router) {}

  goTo(link : number) {
    this.router.navigate(['/video/' + link.toString()]);
  }
}
