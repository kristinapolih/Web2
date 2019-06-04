import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  polasci: any;
  linija: any;

  constructor(private http: HttpClient) { }

  redVoznjeParametri(arg: any): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    let par = {
      redvoznje: arg.redvoznje,
      dan: arg.dan,
      linija: arg.linija
    };
    this.linija = arg.linija;
    return this.http.post<any>('http://localhost:52295/api/RedVoznje/redVoznjeParametri', par, headers).pipe(
      map(res => {
        this.polasci = res;
      })
    );
  }

  getPolasci()
  {
    return this.polasci;
  }

  getTipKarte(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Karta/getTipKarte');
  }

  getDanasnjiDatum(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getDanasnjiDatum');
  }

  getTipRedVoznje(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getTipRedVoznje');
  }

  getTipDana(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getTipDana');
  }

  getLinijeGradske(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinijeGradske');
  }

  getLinijePrigradske(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinijePrigradske');
  }

  isAdmin() {
    if (localStorage.role) {
      if (localStorage.role == "Admin") {
        return true;
      }
      else {
        return false;
      }
    }
    return false;
  }

  isController() {
    if (localStorage.role) {
      if (localStorage.role == "Controller") {
        return true;
      }
      else {
        return false;
      }
    }
    return false;
  }

  isUser() {
    if (localStorage.role) {
      if (localStorage.role == "AppUser") {
        return true;
      }
      else {
        return false;
      }
    }
    return true;
  }

  isUserProfile() {
    if (localStorage.role) {
      if (localStorage.role == "AppUser") {
        return true;
      }
      else {
        return false;
      }
    }
    return false;
  }
}
