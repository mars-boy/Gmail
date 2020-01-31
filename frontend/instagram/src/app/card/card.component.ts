import { Component, OnInit, Input, ViewChild, AfterViewInit, ElementRef } from '@angular/core';
import { CardDetails } from '../models/card-details';
import * as shaka from 'shaka-player';


@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit, AfterViewInit {

  @ViewChild('videoPlayer', null) videoElementRef: ElementRef;
  videoElement: HTMLVideoElement;
  manifestUri = 'http://localhost:5001/StaticContentDir/my_video_manifest.mpd';

  ngAfterViewInit() {
    // Install built-in polyfills to patch browser incompatibilities.
    shaka.polyfill.installAll();

    // Check to see if the browser supports the basic APIs Shaka needs.
    if (shaka.Player.isBrowserSupported()) {
      debugger;
      // Everything looks good!
      this.videoElement = this.videoElementRef.nativeElement;
      this.initPlayer();
    } else {
      // This browser does not have the minimum set of APIs we need.
      console.error('Browser not supported!');
    }
  }

  private initPlayer() {
    let player = new shaka.Player(this.videoElement);
    player.addEventListener('error', this.onErrorEvent);
    player.load(this.manifestUri).then(() => {
      console.log('The video has now been loaded!');
      debugger;
    }).catch(this.onError);
  }

  private onErrorEvent(event) {
    debugger;
    this.onError(event.detail);
  }

  private onError(error) {
    debugger;
    console.error('Error code', error.code, 'object', error);
  }




  @Input() cardDetails : CardDetails;

  constructor() {
  }

  ngOnInit() {
  }

}
