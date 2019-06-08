import { Component, OnInit } from '@angular/core';
import { MainServiceService } from '../main-service.service';
import { Router } from '@angular/router';
import { FormBuilder } from '@angular/forms';

@Component({
  selector: 'app-red-voznje-admin',
  templateUrl: './red-voznje-admin.component.html',
  styleUrls: ['./red-voznje-admin.component.css']
})
export class RedVoznjeAdminComponent implements OnInit {

  linije: any;
  linija: any;
  polasci: any;
  view: boolean = false;

  polasciForm = this.fb.group({
    svipolasci: [""]
  });

  constructor(private fb: FormBuilder, private mainService: MainServiceService, private router: Router) { }

  ngOnInit() {
    this.getLinijeAdmin();
  }

  getLinijeAdmin() {
    this.mainService.getLinijeAdmin().subscribe(
      (res) => {
        this.linije = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  otvoriLiniju(ID:number, ImeRute:string): void {
    this.mainService.getLinijuAdmin(ID).subscribe(
      (res) => {
        this.polasci = res;
        this.linija = ImeRute
        this.view = true;
      });
  }

  View()
  {
    return this.view;
  }

  izmeniLiniju(ID:number, ImeRute:string): void {
    this.mainService.getLinijuAdmin(ID).subscribe(
      (res) => {
        this.polasci = res;
        this.linija = ImeRute
        this.view = false;
        this.polasciForm['svipolasci'] = res;
      });
  }

  onSubmit() {

    this.mainService.izmeniPolaskeAdmin(this.polasciForm.value).subscribe(
      (res) => {
        console.error(res);
      },
      (err) => {
        console.error(err);
      }
    );
  }

}
