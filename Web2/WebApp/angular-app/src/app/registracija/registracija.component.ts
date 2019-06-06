import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from '../main-service.service';

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {
  
  submitted:boolean = false;
  serverSuccessMessage = "";

  registracijaForm = this.fb.group({
    name: ['', [Validators.required, Validators.minLength(3)]],
    surname: ['', [Validators.required, Validators.minLength(3)]],
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required],
    address: ['', Validators.required],
    datumRodjenja: ['', Validators.required],
    tipputnika: ['', Validators.required]
  }, {
      validator: MustMatch('password', 'confirmPassword')
    });

  constructor(private fb: FormBuilder, private mainService:MainServiceService) { }

  get f() { return this.registracijaForm.controls; }

  ngOnInit() {
  }


  onSubmit() {

    this.mainService.registracijaKorisnika(this.registracijaForm.value).subscribe(
      (res) => {
        this.serverSuccessMessage = res;
      }
    );
    this.submitted = true;
  }
}

export function MustMatch(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
    const control = formGroup.controls[controlName];
    const matchingControl = formGroup.controls[matchingControlName];

    if (matchingControl.errors && !matchingControl.errors.mustMatch) {
      return;
    }

    if (control.value !== matchingControl.value) {
      matchingControl.setErrors({ mustMatch: true });
    } else {
      matchingControl.setErrors(null);
    }
  }
}
