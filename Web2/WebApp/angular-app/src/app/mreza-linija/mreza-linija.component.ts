import { Component, OnInit, Output, NgZone } from '@angular/core';
import { MainServiceService } from 'src/app/main-service.service';
import { EventEmitter } from 'protractor';
import { NotificationService } from '../notification.service';

@Component({
  selector: 'app-mreza-linija',
  templateUrl: './mreza-linija.component.html',
  styleUrls: ['./mreza-linija.component.css']
})
export class MrezaLinijaComponent implements OnInit {

  routes: any;
  routesGradske: any;
  routesPrigradske: any;
  sRoute: any;
  isSelected: any = null;
  isConnected: Boolean;
  notifications: string[];
  time: string;
  counter = 0;
  sBus: any;

  constructor(private mainService: MainServiceService, private notifService: NotificationService, private ngZone: NgZone) {
    this.isConnected = false;
    this.notifications = [];
  }

  ngOnInit() {
    this.getHub();
    this.getLinijeGradske();
    this.getLinijePrigradske();
    this.checkConnection();
    this.subscribeForTime();
  }

  selektovanaRuta(route: any) {
    this.notifService.SendBusRoute(route.BrojRute);
    this.isSelected = route;
    this.mainService.getLiniju(route.ID).subscribe(
      (res) => {
        this.sRoute = res;
      });
  }

  getHub() {
    this.mainService.getHub().subscribe(
      (res) => {
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

  private checkConnection() {
    this.notifService.startConnection().subscribe(e => {
    this.isConnected = e;
      if (e) {
        this.notifService.StartTimer()
      }
    });
  }

  subscribeForTime() {
    this.notifService.registerForTimerEvents().subscribe(
      e => {
        this.onTimeEvent(e);
        this.sBus = e;
        console.log(e);
      });
  }

  public onTimeEvent(time: string) {
    this.ngZone.run(() => {
      this.time = time;
    });

  }

  public startTimer() {
    this.notifService.StartTimer();
  }

  public stopTimer() {
    this.notifService.StopTimer();
    this.time = "";
  }

}
