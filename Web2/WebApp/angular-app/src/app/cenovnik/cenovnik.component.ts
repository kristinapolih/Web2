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

  TipKarte : any;
  cena: any;
  cenovnik: any;

  cenovnikForm = this.fb.group({
    tipkarte: ['', Validators.required],
    tipputnika: ['', Validators.required]
  });
  selectedTipKarte = null;
  selectedTipPutnika = null;
  
  coefficients: any;
  selectedPrice: any;
  totalPrice: any;
  typeOfUser: any;
  canBuy: boolean = false;
  isSelectedTicket: boolean = false;

  constructor(private fb: FormBuilder, private mainService:MainServiceService) { }

  get f() { return this.cenovnikForm.controls; }

  ngOnInit() {
    this.getCene();
  }


  PromeniTipKarte()
  {
      if(this.selectedTipKarte == "Vremenska")
      {
        this.cena = this.cenovnik[0];//ne uzima dobro TODO
      }
      else if(this.selectedTipKarte == "Dnevna")
      {
        
      }
      else if(this.selectedTipKarte == "Mesecna")
      {
        
      }
      else{
        
      }
  }


  PromeniTipaPutnika()
  {
    if(this.selectedTipPutnika == "RegularniPutnik")
    {

    }
    else if(this.selectedTipPutnika == "Djak")
    {
      
    }
    else
    {
      
    }
  }
   


  getCene()
  {
    this.mainService.getCene().subscribe(
      (res) => {
        this.cenovnik = res;
      }
    );
  }

  getCoefficeints()
  {
    this.mainService.getCoefficient().subscribe(
      (res) => {
        this.coefficients = res;
      }
    );
  }

  calculatePrice()
  {
    if(this.totalPrice > 0){
        if(this.typeOfUser == "Penzioner")
        {
          this.cena = this.cena*this.coefficients.CoefficientPensioner;
        }
        else if(this.typeOfUser == "Djak")
        {
          this.cena = this.cena*this.coefficients.CoefficientStudent;
        }
    }
  }

  onSubmit() {
    /*if(localStorage.login)
    {
      if(this.cena > 0){
        this.canBuy = false;
        this.isSelectedTicket = false;

        this.mainService.buyTicket(this.typeOfUser, this.selectedRow.type, this.cena).subscribe(
          (res) => {
            console.log(res);
          }
        );
      }
      else
      {
        this.isSelectedTicket = true;
      }
    }
    else
    {
      this.canBuy = true;
    }*/
  }

}
