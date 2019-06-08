import { Component, OnInit, Output } from '@angular/core';
import { MainServiceService } from 'src/app/main-service.service';
import { EventEmitter } from 'protractor';

@Component({
  selector: 'app-mreza-linija',
  templateUrl: './mreza-linija.component.html',
  styleUrls: ['./mreza-linija.component.css']
})
export class MrezaLinijaComponent implements OnInit {

  routes : any;
  routesGradske: any;
  routesPrigradske: any;
  sRoute: any;
  isSelected: any = null;

  constructor(private mainService: MainServiceService) { }

  ngOnInit() {
    this.getLinijeGradske();
    this.getLinijePrigradske();
  }

  selektovanaRuta(route: any) {
    this.isSelected = route;
    this.mainService.getLiniju(route.ID).subscribe(
      (res) => {
        this.sRoute = res;
      });
  }

  getLinijeGradske() {
    this.mainService.getLinijeGradske().subscribe(
      (res) => {
        this.routesGradske = res;
        this.routes = res;
      });
  }

  getLinijePrigradske() {
    this.mainService.getLinijePrigradske().subscribe(
      (res) => {
        this.routesPrigradske = res;
        this.routes = res;
      });
  }

}
