import { Component, OnInit, Input } from '@angular/core';
import { CardDetails } from '../models/card-details';

@Component({
  selector: 'app-card',
  templateUrl: './card.component.html',
  styleUrls: ['./card.component.css']
})
export class CardComponent implements OnInit {


  @Input() cardDetails : CardDetails;

  constructor() { }

  ngOnInit() {
  }

}
