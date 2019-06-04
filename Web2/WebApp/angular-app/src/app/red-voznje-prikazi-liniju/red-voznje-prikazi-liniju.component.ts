import { Component, OnInit } from '@angular/core';
import { MainServiceService } from 'src/app/main-service.service';

@Component({
  selector: 'app-red-voznje-prikazi-liniju',
  templateUrl: './red-voznje-prikazi-liniju.component.html',
  styleUrls: ['./red-voznje-prikazi-liniju.component.css']
})
export class RedVoznjePrikaziLinijuComponent implements OnInit {


  constructor(private mainService: MainServiceService) { }

  ngOnInit() {
  }

  getLiniju() {
    return this.mainService.linija;
  }

  getPolasci() {
    return this.mainService.getPolasci();
  }


}
