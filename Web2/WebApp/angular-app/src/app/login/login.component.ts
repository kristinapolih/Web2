import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from 'src/app/main-service.service';
import { HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  message: string;

  loginForm = this.fb.group({
    email: ['', [Validators.required, Validators.email]],
    pass: ['', Validators.required]
  });

  constructor(private fb: FormBuilder, private mainService:MainServiceService, private http: HttpClient, public authService: AuthService) 
  { 
    this.setMessage();
  }

  setMessage() {
    this.message = 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out');
  }

  get f() { return this.loginForm.controls; }

  ngOnInit() {
  }

  onSubmit() {
    let poruka = this.mainService.login(this.loginForm.value);
    console.warn(this.loginForm.value);
  }

  login() {
    this.authService.login(this.loginForm.value).subscribe((data) => {
      console.log(data);
    });
  }

  logout() {
    this.authService.logout();
    this.setMessage();
  }
}
