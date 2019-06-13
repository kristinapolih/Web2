import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from 'src/app/main-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-promeni-vidi-profil',
  templateUrl: './promeni-vidi-profil.component.html',
  styleUrls: ['./promeni-vidi-profil.component.css']
})
export class PromeniVidiProfilComponent implements OnInit {
  public menjaProfil: boolean = true;
  profilForm: FormGroup;
  submitted: boolean = false;
  serverSuccessMessage = "";
  UserValid: any;
  formValid = false;

  fileToUpload: File = null;
  selectedTipPutnika = null;

  public slika:string;
  id:number;

  constructor(private fb: FormBuilder, private mainService: MainServiceService, private router: Router) { }

  get f() { return this.profilForm.controls; }

  ngOnInit() {

    this.profilForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      surname: ['', [Validators.required, Validators.minLength(3)]],
      email: [''],
      password: [''],
      confirmPassword: [''],
      CurrentPassword: ['', Validators.required],
      address: ['', Validators.required],
      datumRodjenja: ['', Validators.required],
      tipputnika: ['', Validators.required]
    }, {
        validator: MustMatch('password', 'confirmPassword')
      });

    this.getProfil();
    this.slika = ' ';
  };

  getSlika() {
    this.mainService.getSlika(this.id).subscribe(
      (res) => {
        this.slika = 'data:image/png;base64,' + res;
      },
      (err) => {
        console.error(err);
      }
    );
  }

  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  uploadFileToActivity() {
    let e = this.profilForm.get("email").value;
    this.mainService.ubaciSliku(this.fileToUpload, e).subscribe(data => {
    }, error => {
      console.log(error);
    });
  }

  getProfil() {
    this.mainService.getProfil().subscribe(
      (res) => {
        console.log(res);
        this.profilForm.controls['email'].setValue(res["Username"]);
        this.profilForm.controls['name'].setValue(res["Name"]);
        this.profilForm.controls['surname'].setValue(res["Lastname"]);
        this.profilForm.controls['datumRodjenja'].setValue(res["SendBackBirthday"]);
        this.profilForm.controls['address'].setValue(res["Adresa"]);
        this.profilForm.controls['tipputnika'].setValue(res["TipPutnika"]);
        this.id = res.ID;
        this.getSlika();
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
    if (this.profilForm.valid) {
      let poruka = this.mainService.izmeniProfil(this.profilForm.value).subscribe(
        (res) => {
          this.serverSuccessMessage = res;
          if (res === "Profil je uspesno azuriran....") {

            if(this.fileToUpload != null)
            {
              this.uploadFileToActivity();
            }

            setTimeout(() => {
              this.menjaProfil = true;
              this.router.navigate(['/promeni-vidi-profil']);
              this.serverSuccessMessage = "";
            },
              5000);
          }
        }
      );
      this.submitted = true;
    }
    else {
      Object.keys(this.profilForm.controls).forEach(field => {
        const control = this.profilForm.get(field);
        control.markAsTouched({ onlySelf: true });
      });
    }
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