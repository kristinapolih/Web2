import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from 'src/app/main-service.service';

@Component({
  selector: 'app-promeni-vidi-profil',
  templateUrl: './promeni-vidi-profil.component.html',
  styleUrls: ['./promeni-vidi-profil.component.css']
})
export class PromeniVidiProfilComponent implements OnInit {
  public menjaProfil: boolean = true;
  profilForm : FormGroup;
  submitted: boolean = false;
  serverSuccessMessage = "";
  UserValid: any;

  constructor(private fb: FormBuilder, private mainService: MainServiceService) { }

  get f() { return this.profilForm.controls; }

  ngOnInit() {

    this.profilForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      surname: ['', [Validators.required, Validators.minLength(3)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
      CurrentPassword: ['', Validators.required],
      address: ['', Validators.required],
      datumRodjenja: ['', Validators.required],
      tipputnika: ['', Validators.required]
    }, {
        validator: MustMatch('password', 'confirmPassword')
      });

    this.getProfil();
  };

  getProfil() {
    this.mainService.getProfil().subscribe(
      (res) => {
        console.log(res);
       this.profilForm.controls['email'].setValue(res["Username"]);
       this.profilForm.controls['name'].setValue(res["Name"]);
       this.profilForm.controls['surname'].setValue(res["Lastname"]);
       this.profilForm.controls['datumRodjenja'].setValue(res["SendBackBirthday"]);
       this.profilForm.controls['address'].setValue(res["Adresa"]);
       this.profilForm.controls['tipputnika'].setValue(res["UserType"]);

      }
    );

    this.mainService.getTipKorisnika().subscribe(
      (res) => {
        let i: string = res;
        let j: any = JSON.parse(i);
        this.UserValid = j.IsValid;
      }
    );

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
          return;
      }
      if (control.value !== matchingControl.value) {
          matchingControl.setErrors({ mustMatch: true });
      } else {
          matchingControl.setErrors(null);
      }
  }
}