import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from '../main-service.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-registracija',
  templateUrl: './registracija.component.html',
  styleUrls: ['./registracija.component.css']
})
export class RegistracijaComponent implements OnInit {

  submitted: boolean = false;
  serverSuccessMessage = "";

  fileToUpload: File = null;
  selectedTipPutnika = null;

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

  constructor(private fb: FormBuilder, private mainService: MainServiceService, private router: Router) { }

  get f() { return this.registracijaForm.controls; }

  ngOnInit() {
    this.selectedTipPutnika = "Regularni";
  }


  handleFileInput(files: FileList) {
    this.fileToUpload = files.item(0);
  }

  uploadFileToActivity() {
    let e = this.registracijaForm.get("email").value;
    this.mainService.ubaciSliku(this.fileToUpload, e).subscribe(data => {
    }, error => {
      console.log(error);
    });
  }

  onSubmit() {
    if (this.registracijaForm.valid) {
      this.mainService.registracijaKorisnika(this.registracijaForm.value).subscribe(
        (res) => {
          this.serverSuccessMessage = res;
          if (res === "Uspesno registrovani...") {

            if(this.fileToUpload != null)
            {
              this.uploadFileToActivity();
            }

            setTimeout(() => {
              this.router.navigate(['/prijavite-se']);
            },
              5000);
          }
        }
      );
      this.submitted = true;
    }
    else {
      Object.keys(this.registracijaForm.controls).forEach(field => {
        const control = this.registracijaForm.get(field);
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
