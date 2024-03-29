import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

import { Observable, pipe, of } from 'rxjs';
import { catchError, map } from 'rxjs/operators';
import { User } from './user';

@Injectable({
  providedIn: 'root',
})

export class AuthService {
  isLoggedIn = false;

  loginUrl: string = "http://localhost:52295/oauth/token";

  constructor(private http: HttpClient) { }

  login(user: User): Observable<any> {
    let par = "username="+ user.email +"&"+"password="+ user.pass + "&grant_type=password";
    return this.http.post<any>(this.loginUrl, par, { 'headers': { 'Content-type': 'x-www-form-urlencoded' } }).pipe(
      map(res => {
        let jwt = res.access_token;

        let jwtData = jwt.split('.')[1]
        let decodedJwtJsonData = window.atob(jwtData)
        let decodedJwtData = JSON.parse(decodedJwtJsonData)

        let role = decodedJwtData.role

        localStorage.setItem('jwt', jwt)
        localStorage.setItem('role', role);
        localStorage.setItem('login', 'true');

        this.isLoggedIn = true;
      }),

      catchError(this.handleError<any>('login'))
    );
  }

  logout(): void {
    this.isLoggedIn = false;
    localStorage.removeItem('jwt');
    localStorage.removeItem('role');
        localStorage.setItem('login', 'false');
  }

  private handleError<T>(operation = 'operation', result?: T) {
    return (error: any): Observable<T> => {
      return of(result as T);
    };
  }
}
