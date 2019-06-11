import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { MainServiceService } from 'src/app/main-service.service';
import { HttpClient } from '@angular/common/http';
import { AuthService } from 'src/app/auth/auth.service';
import { Router } from '@angular/router';

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

  constructor(private fb: FormBuilder, private router:Router, private mainService:MainServiceService, private http: HttpClient, public authService: AuthService) 
  { 
    this.setMessage();
  }

  get f() { return this.loginForm.controls; }

  ngOnInit() {
  }

  onSubmit() {
    let poruka = this.authService.login(this.loginForm.value).subscribe(
      (res) => {
        console.log(res);
        if(localStorage.role == "AppUser")
        {
          this.router.navigate(['/red-voznje']);
        }
        else if(localStorage.role == "Admin")
        {
          this.router.navigate(['/red-voznje']);
        }
        else if(localStorage.role == "Controller")
        {
          this.router.navigate(['/proveri-karte']);
        }
      }
    );
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

  setMessage() {
    this.message = 'Logged ' + (this.authService.isLoggedIn ? 'in' : 'out');
  }
}
