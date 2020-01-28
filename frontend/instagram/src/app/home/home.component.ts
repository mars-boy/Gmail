import { Component, OnInit } from '@angular/core';
import { CardDetails } from '../models/card-details';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {


  public feeds : CardDetails[] = [];

  constructor() { }

  ngOnInit() {
    this.getFeed();
  }

  getFeed(){
    let tempFeed : CardDetails[] = [];
    debugger;
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    tempFeed.push(new CardDetails("aaron", "ghost", 1));
    this.feeds = this.feeds.concat(tempFeed);
    var pp = [];
  }

  onScrollDown(){
    debugger;
    this.getFeed();
  }
}
