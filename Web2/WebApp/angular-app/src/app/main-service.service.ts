import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  constructor(private http: HttpClient) { }
/*
  login(arg: any): any
  {
    
    console.warn(arg);
    //return this.http.post('http://localhost:52295/api/Account/Logout', '');

    let par = "username=" + arg.email + "&" + "password=" + arg.password + "&grant_type=password";
    return this.http.post('http://localhost:52295/oauth/token',par).subscribe(
      (res)=>console.log(res),
      (err)=>console.log(err)
    );
  }
*/
}
