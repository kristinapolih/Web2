import { Component, OnInit } from '@angular/core';
import { MainServiceService } from '../main-service.service';

@Component({
  selector: 'app-proveri-karte',
  templateUrl: './proveri-karte.component.html',
  styleUrls: ['./proveri-karte.component.css']
})
export class ProveriKarteComponent implements OnInit {

  ok: boolean;
  message: String;
  id: number;

  constructor(private mainService: MainServiceService) { }

  ngOnInit() {
    this.message = new String(" ");
    this.ok = false;
  }

  proveriKartu() {
    let poruka = this.mainService.getKartu(this.id).subscribe(
      (res) => {
        if (res[0] == "Karta je VALIDNA.") {
          this.ok = true;
          this.message = new String(res[0] +'\n'+ res[1] +'\n'+ res[2]);
        }
        else {
          this.ok = false;
          this.message = res[0];
        }
      }
    );
  }

}
