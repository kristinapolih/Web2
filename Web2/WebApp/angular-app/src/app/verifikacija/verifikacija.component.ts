import { Component, OnInit } from '@angular/core';
import { MainServiceService } from '../main-service.service';

@Component({
  selector: 'app-verifikacija',
  templateUrl: './verifikacija.component.html',
  styleUrls: ['./verifikacija.component.css']
})
export class VerifikacijaComponent implements OnInit {

  putnici:any;
  message:string;

  constructor(private mainService: MainServiceService) { }

  ngOnInit() {
    this.getPutnike();
    this.message = "";
  }

  getPutnike()
  {
    this.mainService.getPutnike().subscribe(
      (res) => {
        this.putnici = res;
        
      }
    );
  }

  prihvati(id:number)
  {
    this.mainService.prihvatiKorisnika(id).subscribe(
      (res) => {
        this.message = res;

        setTimeout(() => {
          this.message = "";
          this.getPutnike();
        },  5000);
      }
    );
  }

  odbij(id:number)
  {
    this.mainService.odbijKorisnika(id).subscribe(
      (res) => {
        this.message = res;
        
        setTimeout(() => {
          this.message = "";
          this.getPutnike();
        },  5000);
      }
    );
  }

}
