import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from '../main-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-red-voznje',
  templateUrl: './red-voznje.component.html',
  styleUrls: ['./red-voznje.component.css']
})
export class RedVoznjeComponent implements OnInit {

  danasnjiDatum: any;
  tipRedVoznje: any;
  selectedTipRedVoznje = null;
  tipDana: any;
  selectedtipDana = null;
  tipLinije: any;
  selectedtipLinije = null;

  redVoznjeForm = this.fb.group({
    redvoznje: ['', Validators.required],
    dan: ['', Validators.required],
    linija: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private mainService: MainServiceService, private router: Router) { }

  get f() { return this.redVoznjeForm.controls; }

  getLinijeGradske() {
    this.mainService.getLinijeGradske().subscribe(
      (res) => {
        this.tipLinije = res;
        console.log(res);
      },
      (err) => {
        console.error(err);
      }
    );
  }

  PromeniLiniju()
  {
    if (this.tipRedVoznje == 'Gradski')
    {
      this.getLinijeGradske();
    }
    else
    {
      this.getLinijePrigradske();
    }
  }

  getLinijePrigradske() {
    this.mainService.getLinijePrigradske().subscribe(
      (res) => {
        this.tipLinije = res;
        console.log(res);
      },
      (err) => {
        console.error(err);
      }
    );
  }

  getTipDana() {
    this.mainService.getTipDana().subscribe(
      (res) => {
        this.tipDana = res;
        this.selectedtipDana = this.tipDana[0];
        console.log(res);
      },
      (err) => {
        console.error(err);
      }
    );
  }

  getDanasnjiDatum() {
    this.mainService.getDanasnjiDatum().subscribe(
      (res) => {
        this.danasnjiDatum = res;
        console.log(res);
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
        console.log(res);
      },
      (err) => {
        console.error(err);
      }
    );
  }

  ngOnInit() {
    this.getDanasnjiDatum();
    this.getTipRedVoznje();
    this.getTipDana();
    this.getLinijeGradske();
    //this.getLinijePrigradske();
  }

  onSubmit() {

    this.mainService.redVoznjeParametri(this.redVoznjeForm.value).subscribe(
      (res) => {

      },
      (err) => {
        console.error(err);
      }
    );

    console.warn(this.redVoznjeForm.value);
  }

  showLine(){
    this.router.navigateByUrl("red-voznje/linija");
  }

}
