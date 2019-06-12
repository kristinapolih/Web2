import { Component, OnInit } from '@angular/core';
import { MainServiceService } from '../main-service.service';
import { Router } from '@angular/router';
import { FormBuilder, Validators } from '@angular/forms';

@Component({
  selector: 'app-red-voznje-admin',
  templateUrl: './red-voznje-admin.component.html',
  styleUrls: ['./red-voznje-admin.component.css']
})
export class RedVoznjeAdminComponent implements OnInit {

  linije: any;
  line: any;
  polasci: any;

  view: boolean = false;
  izmeni: boolean = false;
  obrisi: boolean = false;
  novaLinija: boolean = true;

  message: any;
  idlinije: number;

  tipDana: any;
  selectedtipDana = null;
  tipRedVoznje: any;
  selectedTipRedVoznje = null;

  polasciForm = this.fb.group({
    svipolasci: [""]
  });

  naslovForm = this.fb.group({
    naslov: [""]
  });

  danForm = this.fb.group({
    dan: [""]
  });

  novaLinijaForm = this.fb.group({
    naslovNew: ['', Validators.required],
    danNew: [""],
    redvoznje: ["", Validators.required],
    svipolasciNew: ["", Validators.required]
  });

  constructor(private fb: FormBuilder, private mainService: MainServiceService, private router: Router) { }

  ngOnInit() {
    this.getLinijeAdmin();
  }

  dodajNovuLiniju() {
    this.message = "";
    this.novaLinija = false;
    this.getTipRedVoznje();
    this.getTipDana();
    this.novaLinijaForm['naslovNew'] = '';
    this.novaLinijaForm['svipolasciNew'] = "";
    this.view = false;
    this.izmeni = false;
    this.obrisi = false;
  }

  dodajLiniju() {
    if (this.novaLinijaForm.valid) {
      this.mainService.dodajNovuLinijuAdmin(this.novaLinijaForm.value).subscribe(
        (res) => {
          this.obrisi = true;
          this.message = res;
          this.novaLinija = true;
          this.getLinijeAdmin();
          if (res === "Nova linija je uspešno dodata...") {

            setTimeout(() => {
              this.router.navigate(['/red-voznje-admin']);
            },
              5000);
          }
        }
      );
    }
    else {
      Object.keys(this.novaLinijaForm.controls).forEach(field => {
        const control = this.novaLinijaForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }

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

  otvoriLiniju(ID: number, ImeRute: string): void {
    this.mainService.getLinijuListAdmin(ID).subscribe(
      (res) => {
        this.message = "";
        this.polasci = res;
        this.line = ImeRute
        this.view = true;
        this.izmeni = false;
        this.obrisi = false;
      });
  }

  View() {
    return this.view;
  }

  izmeniLiniju(ID: number, ImeRute: string): void {
    this.mainService.getLinijuAdmin(ID).subscribe(
      (res) => {
        this.message = "";
        this.polasci = res;
        this.line = ImeRute
        this.view = false;
        this.izmeni = true;
        this.obrisi = false;
        this.polasciForm['svipolasci'] = res;
        this.naslovForm['naslov'] = ImeRute;
        this.idlinije = ID;
        this.getTipDana();
      });
  }

  onSubmit() {
    this.mainService.izmeniPolaskeAdmin(this.polasciForm.value, this.idlinije).subscribe(
      (res) => {
        this.message = res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  onSubmitNaslov() {
    this.mainService.izmeniImeLinijeAdmin(this.line, this.idlinije).subscribe(
      (res) => {
        this.message = res;
        this.getLinijeAdmin();
      },
      (err) => {
        console.error(err);
      }
    );
  }

  onSubmitDan() {
    this.mainService.izmeniDanLinijeAdmin(this.selectedtipDana, this.idlinije).subscribe(
      (res) => {
        this.message = res;
        this.getLinijeAdmin();
      },
      (err) => {
        console.error(err);
      }
    );
  }

  obrisiLinijuAdmin(ID: number): void {
    this.message = "";
    this.mainService.obrisiLinijuAdmin(ID).subscribe(
      (res) => {
        this.message = res;
        this.view = false;
        this.izmeni = false;
        this.obrisi = true;
        this.getLinijeAdmin();
      });
  }

  getTipDana() {
    this.mainService.getTipDana().subscribe(
      (res) => {
        this.tipDana = res;
        this.selectedtipDana = this.tipDana[0];
      },
      (err) => {
        console.error(err);
      }
    );
  }

  getTipRedVoznje() {
    this.mainService.getTipRedVoznje().subscribe(
      (res) => {
        this.tipRedVoznje = res;
        this.selectedTipRedVoznje = this.tipRedVoznje[0];
      },
      (err) => {
        console.error(err);
      }
    );
  }

}
