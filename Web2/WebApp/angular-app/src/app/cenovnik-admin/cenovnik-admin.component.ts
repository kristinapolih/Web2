import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from '../main-service.service';
import { CenovnikHelp } from './cenovnik';

@Component({
  selector: 'app-cenovnik-admin',
  templateUrl: './cenovnik-admin.component.html',
  styleUrls: ['./cenovnik-admin.component.css']
})
export class CenovnikAdminComponent implements OnInit {

  cenovnici:any;

  message: string = "";

  view: boolean = false;
  izmeni: boolean = false;
  novCenovnik: boolean = true;

  cenovnik: CenovnikHelp;

  novCenovnikForm = this.fb.group({
    odDatuma: ['', Validators.required],
    doDatuma: ['', Validators.required],
    vremenska: ['', Validators.required],
    dnevna: ['', Validators.required],
    mesecna: ['', Validators.required],
    godisnja: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private mainService: MainServiceService) { }

  get f() { return this.novCenovnikForm.controls; }

  ngOnInit() {
    this.getCenovnike();
  }
  
  otvoriCenovnik(ID: number): void {
    this.mainService.getCenovnik(ID).subscribe(
      (res) => {
        this.cenovnik = res;
        this.novCenovnikForm.controls['odDatuma'].setValue(res["OdDatuma"]);
        this.novCenovnikForm.controls['doDatuma'].setValue(res["DoDatuma"]);
        this.novCenovnikForm.controls['vremenska'].setValue(res["VremenskaCena"]);
        this.novCenovnikForm.controls['dnevna'].setValue(res["DnevnaCena"]);
        this.novCenovnikForm.controls['mesecna'].setValue(res["MesecnaCena"]);
        this.novCenovnikForm.controls['godisnja'].setValue(res["GodisnjaCena"]);
        this.message = "";
        this.view = true;
        this.novCenovnik = true;
        this.izmeni = false;
      });
  }

  izmeniCenovnik(){
    //TODO
  }

  dodajNovCenovnik(){
    this.message = "";
    this.novCenovnik = false;
    this.view = false;
    this.izmeni = false;

    this.novCenovnikForm.controls['odDatuma'].setValue("");
        this.novCenovnikForm.controls['doDatuma'].setValue("");
        this.novCenovnikForm.controls['vremenska'].setValue("");
        this.novCenovnikForm.controls['dnevna'].setValue("");
        this.novCenovnikForm.controls['mesecna'].setValue("");
        this.novCenovnikForm.controls['godisnja'].setValue("");
  }

  getCenovnike() {
    this.mainService.getCenovnike().subscribe(
      (res) => {
        this.cenovnici = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  onSubmit() {
    if (this.novCenovnikForm.valid) {
      this.mainService.dodajCenovnik(this.novCenovnikForm.value).subscribe(
        (res) => {
          this.message = res;
        }
      );
    }
    else {
      Object.keys(this.novCenovnikForm.controls).forEach(field => {
        const control = this.novCenovnikForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

}
