import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from '../main-service.service';
import { CenovnikHelp } from './cenovnik';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cenovnik-admin',
  templateUrl: './cenovnik-admin.component.html',
  styleUrls: ['./cenovnik-admin.component.css']
})
export class CenovnikAdminComponent implements OnInit {

  cenovnici: any;

  message: string = "";

  view: boolean = false;
  izmeni: boolean = false;
  novCenovnik: boolean = true;
  menjaDatum: boolean = false;

  idcenovnik: number;

  cenovnik: CenovnikHelp;

  novCenovnikForm = this.fb.group({
    odDatuma: ['', Validators.required],
    doDatuma: ['', Validators.required],
    vremenska: ['', Validators.required],
    dnevna: ['', Validators.required],
    mesecna: ['', Validators.required],
    godisnja: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private mainService: MainServiceService, private router: Router) { }

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
        this.menjaDatum = false;
      });
  }

  izmeniCenovnik(ID: number): void {
    this.mainService.getCenovnikIzmena(ID).subscribe(
      (res) => {
        if (res.Menja == true) {
          this.idcenovnik = ID;
          this.menjaDatum = true;
          this.novCenovnik = true;
          this.izmeni = true;
          this.view = true;
          this.novCenovnikForm.controls['odDatuma'].setValue(res["OdDatuma"]);
          this.novCenovnikForm.controls['doDatuma'].setValue(res["DoDatuma"]);
          this.novCenovnikForm.controls['vremenska'].setValue(res["VremenskaCena"]);
          this.novCenovnikForm.controls['dnevna'].setValue(res["DnevnaCena"]);
          this.novCenovnikForm.controls['mesecna'].setValue(res["MesecnaCena"]);
          this.novCenovnikForm.controls['godisnja'].setValue(res["GodisnjaCena"]);
        }
        else
        {
          this.message = "Ovaj cenovnik je istekao ne možete ga menjati....";
          this.izmeni = false;
        this.menjaDatum = false;
        this.view = false;
        this.novCenovnik = true;
        }
      });
  }

  dodajNovCenovnik() {
    this.message = "";
    this.novCenovnik = false;
    this.view = false;
    this.izmeni = false;
    this.menjaDatum = false;

    this.novCenovnikForm = this.fb.group({
      odDatuma: ['', Validators.required],
      doDatuma: ['', Validators.required],
      vremenska: ['', Validators.required],
      dnevna: ['', Validators.required],
      mesecna: ['', Validators.required],
      godisnja: ['', Validators.required]
    });
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
      if (!this.izmeni) {
        this.mainService.dodajCenovnik(this.novCenovnikForm.value).subscribe(
          (res) => {
            this.message = res;
            this.getCenovnike();

            setTimeout(() => {
              this.message = "";
              this.view = false;
              this.novCenovnik = true;
              this.izmeni = false;
              this.menjaDatum = false;
            }, 5000);
          }
        )
      }
      else {
        this.mainService.izmeniCenovnik(this.novCenovnikForm.value, this.idcenovnik).subscribe(
          (res) => {
            this.message = res;
            this.getCenovnike();

            setTimeout(() => {
              this.message = "";
              this.view = false;
              this.novCenovnik = true;
              this.izmeni = false;
              this.menjaDatum = false;
            }, 5000);
          }
        )
      }
    }
    else {
      Object.keys(this.novCenovnikForm.controls).forEach(field => {
        const control = this.novCenovnikForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
  }

}
