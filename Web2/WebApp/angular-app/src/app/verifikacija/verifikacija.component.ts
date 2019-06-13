import { Component, OnInit } from '@angular/core';
import { MainServiceService } from '../main-service.service';

@Component({
  selector: 'app-verifikacija',
  templateUrl: './verifikacija.component.html',
  styleUrls: ['./verifikacija.component.css']
})
export class VerifikacijaComponent implements OnInit {

  putnici: any;
  message: string;
  messageKorisnik: string;

  slikaBool: boolean = false;
  public slika: string;

  constructor(private mainService: MainServiceService) { }

  ngOnInit() {
    this.getPutnike();
    this.slikaBool = false;
    this.slika = "";
    this.messageKorisnik = "";
  }

  getPutnike() {
    this.mainService.getPutnike().subscribe(
      (res) => {
        this.putnici = res;

      }
    );
  }

  prihvati(id: number) {
    this.slikaBool = false;
    this.slika = "";
    this.messageKorisnik = "";
    this.mainService.prihvatiKorisnika(id).subscribe(
      (res) => {
        this.message = res;

        setTimeout(() => {
          this.message = "";
          this.getPutnike();
        }, 5000);
      }
    );
  }

  odbij(id: number) {
    this.slikaBool = false;
    this.slika = "";
    this.messageKorisnik = "";
    this.mainService.odbijKorisnika(id).subscribe(
      (res) => {
        this.message = res;

        setTimeout(() => {
          this.message = "";
          this.getPutnike();
        }, 5000);
      }
    );
  }

  otvoriSliku(id: number, email: string) {
    this.slikaBool = true;
    this.mainService.getSlika(id).subscribe(
      (res) => {
        if (res == "Nema slike") {
          this.slikaBool = false;
          this.slika = "";
          this.messageKorisnik = "Korisnik [Email: " + email + "] nema priloženu sliku/dokument";
        }
        else {
          this.slika = 'data:image/png;base64,' + res;
          this.messageKorisnik = "Korisnik [Email: " + email + "]";
        }
      },
      (err) => {
        console.error(err);
      }
    );
  }

}
