import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-navigation-buttons',
  templateUrl: './navigation-buttons.component.html',
  styleUrls: ['./navigation-buttons.component.css']
})
export class NavigationButtonsComponent implements OnInit {

appTitle: string = 'BusNS';

  constructor() { }

  ngOnInit() {
  }

}
