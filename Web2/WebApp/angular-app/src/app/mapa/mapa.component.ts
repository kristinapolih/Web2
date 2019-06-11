import { Component, OnInit, Input, NgZone } from '@angular/core';
import { MarkerInfo } from './model/marker-info.model';
import { GeoLocation } from './model/geolocation';
import { Polyline } from './model/polyline';
import { Marker } from '@agm/core/services/google-maps-types';

@Component({
  selector: 'app-mapa',
  templateUrl: './mapa.component.html',
  styleUrls: ['./mapa.component.css'],
  styles: ['agm-map {height: 500px; width: 700px;}']
})
export class MapaComponent implements OnInit {

  public polyline: Polyline;
  public stationsIcon: MarkerInfo[];
  public busLocation: MarkerInfo[];
  public zoom: number;
  private _route: any;
  private _bus:  any;

  markerInfo: MarkerInfo;

  ngOnInit() {
    this.polyline= new Polyline([], '#369691', { url:"", scaledSize: {width: 50, height: 50}});
  }

  constructor(private ngZone: NgZone){
  }

  @Input()
  set route(route: any) {
    this._route = route;
    if(this._route){
      this.busLocation = undefined;
        this.drowRoutes();
    }
  }

  @Input()
  set bus(bus: any) {
    this._bus = bus;
    if(this._bus){
       this.drowBus();
    }
  }

  HaveBus()
  {
    if(this.busLocation)
    {
      return true;
    }
    else
    {
      return false;
    }
  }
  
  drowBus()
  {
    this.busLocation = [];
    this._bus.forEach(element => {
      console.log(element);
      this.busLocation.push(new MarkerInfo(new GeoLocation(element.X, element.Y), "assets/BusMarker.png","","",""));
    });
  }

  drowRoutes()
  {
    let count = 0;
    this.polyline= new Polyline([], '#369691', { url:"", scaledSize: {width: 50, height: 50}});
    this.stationsIcon = [];
    this._route.Stanice.forEach(element => {
      if(!element.IsStanica) 
      {
        this.polyline.addLocation(new GeoLocation(element.X, element.Y));
      }
      else
      {
        this.stationsIcon.push(new MarkerInfo(new GeoLocation(element.X, element.Y), "assets/busicon.png",
        element.Naziv , element.Adresa.Grad +  ", "+ element.Adresa.Ulica + ", " + element.Adresa.Broj,""));
      }
    });
  }

  placeMarker($event){
    if(localStorage.role == "Admin"){
      this.polyline.addLocation(new GeoLocation($event.coords.lat, $event.coords.lng));
      console.log(this.polyline);
    }
  }

}
