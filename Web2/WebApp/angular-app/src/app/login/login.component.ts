import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  loginForm = this.fb.group({
    email: ['', Validators.required, Validators.email],
    pass: ['', Validators.required]
  });

  constructor(private fb: FormBuilder) { }

  get f() { return this.loginForm.controls; }

  ngOnInit() {
  }

  onSubmit() {
    console.warn(this.loginForm.value);
  }
}
