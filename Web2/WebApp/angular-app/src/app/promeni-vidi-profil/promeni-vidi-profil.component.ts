import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-promeni-vidi-profil',
  templateUrl: './promeni-vidi-profil.component.html',
  styleUrls: ['./promeni-vidi-profil.component.css']
})
export class PromeniVidiProfilComponent implements OnInit {
  public menjaProfil: boolean = true;

  profilForm = this.fb.group({
    name: ['', Validators.required, Validators.minLength(3)],
    surname: ['', Validators.required, Validators.minLength(3)],
    email: ['', Validators.required, Validators.email],
    pass: ['', Validators.required],
    repeatpass: ['', Validators.required],
    address: ['', Validators.required],
    datumRodjenja: ['', Validators.required],
    tipputnika: ['', Validators.required]
  },{
    validator: MustMatch('pass', 'repeatpass')
 });

  constructor(private fb: FormBuilder) { }

  get f() { return this.profilForm.controls; }

  ngOnInit() {
  }
   

  onSubmit() {
    console.warn(this.profilForm.value);
  }

  MenjaProfil() {
    this.menjaProfil = false;
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