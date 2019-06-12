import { Component, OnInit } from '@angular/core';
import { Validators, FormBuilder, FormGroup } from '@angular/forms';
import { Router } from '@angular/router';
import { MainServiceService } from '../main-service.service';

@Component({
  selector: 'app-dodaj-kontrolera-admin',
  templateUrl: './dodaj-kontrolera-admin.component.html',
  styleUrls: ['./dodaj-kontrolera-admin.component.css']
})
export class DodajKontroleraAdminComponent implements OnInit {

  message: string;

  dodajForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', Validators.required],
    confirmPassword: ['', Validators.required]
  }, {
    validator: MustMatch('password', 'confirmPassword')
  });

  constructor(private fb: FormBuilder, private router:Router, private mainService:MainServiceService) { }

  get f() { return this.dodajForm.controls; }

  ngOnInit() {
  }

  onSubmit() {
    if (this.dodajForm.valid) {
      this.mainService.dodajKontrolera(this.dodajForm.value).subscribe(
        (res) => {
          this.message = res;
          if (res === "Uspesno ste registrovali kotrolera...") {

            setTimeout(() => {
              this.message = "";
              this.dodajForm = this.fb.group({
                email: ['', [Validators.required, Validators.email]],
                password: ['', Validators.required],
                confirmPassword: ['', Validators.required]
              }, {
                validator: MustMatch('password', 'confirmPassword')
              });
              this.router.navigate(['/dodaj-kotrolera-admin']);
            }, 5000);
          }
        }
      );
    }
    else {
      Object.keys(this.dodajForm.controls).forEach(field => {
        const control = this.dodajForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
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