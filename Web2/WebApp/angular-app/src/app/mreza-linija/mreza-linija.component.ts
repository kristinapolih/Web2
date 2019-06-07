import { Component, OnInit, Output } from '@angular/core';
import { MainServiceService } from 'src/app/main-service.service';
import { EventEmitter } from 'protractor';

@Component({
  selector: 'app-mreza-linija',
  templateUrl: './mreza-linija.component.html',
  styleUrls: ['./mreza-linija.component.css']
})
export class MrezaLinijaComponent implements OnInit {

  routes: any;
  sRoute: any;
  isSelected: any = null;

  constructor(private mainService: MainServiceService) { }

  ngOnInit() {
    this.getLinije();
  }

  selektovanaRuta(route: any) {
    this.isSelected = route;
    this.mainService.getLiniju(route.ID).subscribe(
      (res) => {
        console.log(res);
        this.sRoute = res;
      });
  }

  getLinije() {
    this.mainService.getLinije().subscribe(
      (res) => {
        this.routes = res;
      });
  }

}
