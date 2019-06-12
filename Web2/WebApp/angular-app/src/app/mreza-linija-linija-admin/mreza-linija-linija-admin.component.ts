import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder } from '@angular/forms';
import { Polyline } from '../mapa/model/polyline';
import { GeoLocation } from '../mapa/model/geolocation';
import { MainServiceService } from '../main-service.service';

@Component({
  selector: 'app-mreza-linija-linija-admin',
  templateUrl: './mreza-linija-linija-admin.component.html',
  styleUrls: ['./mreza-linija-linija-admin.component.css']
})
export class MrezaLinijaLinijaAdminComponent implements OnInit {
  
  constructor(private fb: FormBuilder, private mainService: MainServiceService) {
   }


  ngOnInit() {
  }

}
