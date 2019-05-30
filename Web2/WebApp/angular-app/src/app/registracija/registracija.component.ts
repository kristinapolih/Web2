import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import {AbstractControl} from '@angular/forms';

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {
//ime, prezime, pass pass, email, adresa, datum rodjenja, type of user, slika -->
  registracijaForm = this.fb.group({
    name: ['', Validators.required, Validators.name, Validators.minLength(3)],
    surname: ['', Validators.required, Validators.name, Validators.minLength(3)],
    email: ['', Validators.required, Validators.email],
    pass: ['', Validators.required],
    repeatpass: ['', Validators.required],
    address: ['', Validators.required],
    datumRodjenja: ['', Validators.required]
  },{
    validator: MustMatch('pass', 'repeatpass')
 });

  constructor(private fb: FormBuilder) { }

  get f() { return this.registracijaForm.controls; }

  ngOnInit() {
  }
   

  onSubmit() {
    console.warn(this.registracijaForm.value);
  }
}

export function MustMatch(controlName: string, matchingControlName: string) {
  return (formGroup: FormGroup) => {
      const control = formGroup.controls[controlName];
      const matchingControl = formGroup.controls[matchingControlName];

      if (matchingControl.errors && !matchingControl.errors.mustMatch) {
          // return if another validator has already found an error on the matchingControl
          return;
      }

      // set error on matchingControl if validation fails
      if (control.value !== matchingControl.value) {
          matchingControl.setErrors({ mustMatch: true });
      } else {
          matchingControl.setErrors(null);
      }
  }
}
