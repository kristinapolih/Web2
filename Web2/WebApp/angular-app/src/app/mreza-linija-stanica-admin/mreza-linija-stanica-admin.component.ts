import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder } from '@angular/forms';
import { MainServiceService } from '../main-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-mreza-linija-stanica-admin',
  templateUrl: './mreza-linija-stanica-admin.component.html',
  styleUrls: ['./mreza-linija-stanica-admin.component.css']
})
export class MrezaLinijaStanicaAdminComponent implements OnInit {

  newStation: boolean;
  newStations: boolean;
  changeStationbool: boolean;
  deleteStationAdminbool: boolean;
  newStationbool: boolean;
  newStationsbool: boolean;

  listRoutesNewRoute: any;
  selectedRouteNewRoute: any;
  stations: any;
  selectedRouteDel: any;
  listRoutes : any;
  selectedRouteAddStation : any;
  listRoutesAddStation: any;

  sType: any;
  sRoute : any;
  sXY: any;
  sXYDot: any[];

  message: string;

  deleteStationForm = this.fb.group({
    IdStation: [''],
    DeleteStationRoute: ['', Validators.required],
  });

  changeStationForm = this.fb.group({
    IdStation: [''],
    Name: ['', Validators.required],
  });

  addStationForm = this.fb.group({
    Name: ['', Validators.required],
    Address: ['', Validators.required],
    RouteNumber: ['', Validators.required],
    IdRoute: ['', Validators.required],
    X: ['', Validators.required],
    Y: ['', Validators.required],
    RouteNumbers: ['', Validators.required],
    NumberInRoute: ['']
  });

  constructor(private fb: FormBuilder, private mainService: MainServiceService, private router: Router) { }

  get f() { return this.changeStationForm.controls; }
  get d() { return this.addStationForm.controls; }

  ngOnInit() {
    this.sXYDot = [];
    this.newStation = true;
    this.newStations = true;
    this.changeStationbool = false;
    this.deleteStationAdminbool = false;
    this.newStationbool = false;
    this.newStationsbool = false;
    this.getAllStations();
  }

  getAllStations() {
    this.mainService.getStationsAdmin().subscribe(
      (res) => {
        this.stations = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  changeStation(id: number, name: string) {
    this.changeStationForm.controls['IdStation'].setValue(id);
    this.changeStationForm.controls['Name'].setValue(name);
    this.newStation = true;
    this.newStations = true;
    this.changeStationbool = true;
    this.deleteStationAdminbool = false;
    this.newStationbool = false;
    this.newStationsbool = false;
  }

  onSubmitSaveChanges() {
    this.mainService.onSubmitSaveChanges(this.changeStationForm.value).subscribe(
      (res) => {
        if (res == "Stanica je uspešno izmenjena...") {
          this.message = res;
          setTimeout(() => {
            this.ngOnInit();
          }, 5000);
        }
      },
      (err) => {
        console.error(err);
      }
    );
  }

  deleteRouteAdmin(station: any) {
    this.listRoutes = station.BrojeviRuta;
    this.deleteStationForm.controls['IdStation'].setValue(station["ID"]);
    this.selectedRouteDel = this.listRoutes[0];
    this.newStation = true;
    this.newStations = true;
    this.changeStationbool = false;
    this.deleteStationAdminbool = true;
    this.newStationbool = false;
    this.newStationsbool = false;
  }

  onSubmitSaveDelete() {
    let p = this.deleteStationForm.value;

    let pom = this.stations.filter(function (s) {
      return s.ID == p.IdStation;
    });

    let pom2 = 0;
    let bb = true;
    
    pom[0].BrojeviRuta.forEach(element => {
      if (element == this.selectedRouteDel) {
        bb = false;
      }

      if (bb) {
        pom2 += 1;
      }
    });

    let fpom = pom[0].IDRute[pom2];

    p.BrojRute = fpom;

    this.mainService.deleteStationFromRoute(p).subscribe(
      (res) => {
        console.log(res);
      });
  }

  addNewStation() {
    this.newStation = false;
    this.newStations = true;
    this.changeStationbool = false;
    this.deleteStationAdminbool = false;
    this.newStationbool = true;
    this.newStationsbool = false;

    this.mainService.getRoutesAddStation().subscribe(
      (res) => {
        this.listRoutesAddStation = res;
        this.selectedRouteAddStation = this.listRoutesAddStation[0];
        this.mapDrow(this.listRoutesAddStation[0].ID);
        this.sType = true;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  mySelectHandler() {
    this.mapDrow(this.selectedRouteAddStation.ID);
    this.sType = true;
  }

  mapDrow(id: any) {
    this.mainService.getLiniju(id).subscribe(
      (res) => {
        this.sRoute = res;
      });
  }

  addNewStations() {
    this.newStation = true;
    this.newStations = false;
    this.changeStationbool = false;
    this.deleteStationAdminbool = false;
    this.newStationbool = false;
    this.newStationsbool = true;

    this.mainService.getNewRoutes().subscribe(
      (res) => {
        this.listRoutesNewRoute = res;
        this.selectedRouteNewRoute = this.listRoutesNewRoute[0];
      });
  }

  XYData(data: any) {
    this.sXY = data;
  }

  XYDataDot(data: any) {
    console.log(data);
    this.sXYDot.push(data);
  }

  onSubmitAddStation() {
    let routeNumberPom: any;
    //let idRoutePom:any;
    let pom: any[];
    pom = [];
    pom.push(this.addStationForm.value["RouteNumbers"]);

    this.addStationForm.value["RouteNumbers"] = pom;
   // idRoutePom =  this.addStationForm.value["RouteNumber"]["Id"];
    routeNumberPom = this.addStationForm.value["RouteNumber"];
    pom = [];
    pom.push(this.selectedRouteAddStation.ID);
    //this.addStationForm["RouteNumber"].setValue(routeNumberPom);
    this.addStationForm.controls['RouteNumber'].setValue(routeNumberPom);
    this.addStationForm.controls['IdRoute'].setValue(pom);
    //NumberInRoute
    this.addStationForm.controls['NumberInRoute'].setValue(this.addStationForm.value["RouteNumbers"][0]);
    this.addStationForm.controls['X'].setValue(this.sXY.X);
    this.addStationForm.controls['Y'].setValue(this.sXY.Y);
    console.log(this.addStationForm.value);

    this.mainService.addStation(this.addStationForm.value).subscribe(
      (res) => {
        console.log(res);
      });
  }

  onSubmitSaveRouteLines() {
    //sXYDot
    console.log(this.sXYDot);
    console.log(this.selectedRouteNewRoute);
    //selectedRouteNewRoute

    //nova klasa in string lista 
    let pom = {
      "Id": this.selectedRouteNewRoute.ID, "BrojRute": this.selectedRouteNewRoute.RouteNumber,
      "Dots": this.sXYDot
    }
    console.log(pom);
    this.mainService.addLines(pom).subscribe(
      (res) => {
        console.log(res);
      });
  }
}
