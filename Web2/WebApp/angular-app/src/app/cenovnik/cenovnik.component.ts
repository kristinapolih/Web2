import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from '../main-service.service';

@Component({
  selector: 'app-cenovnik',
  templateUrl: './cenovnik.component.html',
  styleUrls: ['./cenovnik.component.css']
})
export class CenovnikComponent implements OnInit {

  TipKarte: any;
  cena: any;
  cenovnik: Array<string>;

  cenovnikForm = this.fb.group({
    tipkarte: ['', Validators.required],
    tipputnika: ['', Validators.required]
  });
  selectedTipKarte = null;
  selectedTipPutnika = null;

  canBuy: boolean = false;
  typeOfUser: any = "Regular";
  typeOfLoginUser: any;
  purchasedTickets: any;

  textMessage: string = "";

  coefficients: any;

  constructor(private fb: FormBuilder, private mainService: MainServiceService) { }

  get f() { return this.cenovnikForm.controls; }

  ngOnInit() {

    if (localStorage.role == "AppUser") {
      this.getKarte();
      this.getTipKorisnika();
    }

    this.getCene();
    this.getKoeficijente();

    this.selectedTipKarte = "Vremenska";
    this.selectedTipPutnika = "RegularniPutnik";
    this.PromeniTipaPutnika();
  }

  get Cena() { return this.cena; }

  obrisiKartu(u) {
    this.mainService.obrisiKartu(u).subscribe(
      (res) => {
        this.getKarte();
      });
  }

  getTipKorisnika() {
    this.mainService.getTipKorisnika().subscribe(
      (res) => {
        let i: string = res;
        let j: any = JSON.parse(i);
        this.typeOfLoginUser = j;
      }
    );
  }

  getKarte() {
    this.mainService.getKarte().subscribe(
      (res) => {
        this.purchasedTickets = res;
        console.log(res);
      }
    );
  }

  PromeniTipKarte() {
    if (this.selectedTipKarte == "Vremenska") {
      this.cena = this.cenovnik[0];
    }
    else if (this.selectedTipKarte == "Dnevna") {
      this.cena = this.cenovnik[1];
    }
    else if (this.selectedTipKarte == "Mesecna") {
      this.cena = this.cenovnik[2];
    }
    else if (this.selectedTipKarte == "Godisnja") {
      this.cena = this.cenovnik[3];
    }
  }


  PromeniTipaPutnika() {
    if (this.selectedTipPutnika == "RegularniPutnik") {
      this.PromeniTipKarte();
    }
    else if (this.selectedTipPutnika == "Djak") {
      this.PromeniTipKarte();
      if (this.cena > 0) {
        this.cena = this.cena * this.coefficients.Djak;
      }
    }
    else if (this.selectedTipPutnika == "Penzioner") {
      this.PromeniTipKarte();
      if (this.cena > 0) {
        this.cena = this.cena * this.coefficients.Pensioner;
      }
    }
  }

  getCene() {
    this.mainService.getCene().subscribe(
      (res) => {
        this.cenovnik = res;
      }
    );
  }

  getKoeficijente() {
    this.mainService.getKoeficijente().subscribe(
      (res) => {
        this.coefficients = res;
      }
    );
  }

  onSubmit() {
    this.getTipKorisnika();
    if (localStorage.login) {
      if (localStorage.role == "AppUser") {
        if (this.selectedTipPutnika.toLowerCase() == this.typeOfLoginUser.TypeOfUser.toLowerCase() || this.selectedTipPutnika == "RegularniPutnik") {
          if (this.typeOfLoginUser.IsValid == "Prihvacen") {
            this.canBuy = false;
            this.mainService.kupiKartu(this.selectedTipPutnika, this.selectedTipKarte, this.cena).subscribe(
              (res) => {
                this.textMessage = res;
                this.getKarte();
              }
            );

            this.getKarte();
          }
          else {
            this.textMessage = "Niste verifikovani....";
          }
        }
        else {
          this.textMessage = "Ne možete da izaberete ovaj popust....";
        }
      }
      else {
        this.textMessage = "Samo ulogovani i verifikovani korisnici mogu da kupuju karte....";
      }
    }
    else {
      this.canBuy = true;
    }
  }

}
