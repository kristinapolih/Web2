import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';
import { Karta } from 'src/app/cenovnik/karta';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  polasci: any;
  linija: any;

  constructor(private http: HttpClient) { }

  getLinije(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinije');
  }

  getLiniju(id: number): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLiniju' + `/?id=${id}`);
  }

  obrisiKartu(u: any): Observable<any>{
    return this.http.post<any>('http://localhost:52295/api/Karta/obrisiKartu',u);
  } 

  getKarte(): Observable<any>{
    return this.http.get<any>('http://localhost:52295/api/Karta/getKarte');
  }

  getCene(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Karta/getCene');
  }

  getKoeficijente(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Karta/getKoeficijente');
  }

  kupiKartu(tipKorisnika: string, tipKarte: string, cena: number) : Observable<any>
  {
    let karta: Karta = new Karta();
    karta.tipKorisnika = tipKorisnika;
    karta.tipKarte = tipKarte;
    karta.cena = cena;
    return this.http.post<any>('http://localhost:52295/api/Karta/kupiKartu',karta);
  }

  getProfil(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/Registracija/getProfil');
  }

  getTipKorisnika(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Registracija/getTipKorisnika');
  }

  registracijaKorisnika(arg: any): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    let par = {
      username: arg.email,
      password: arg.password,
      name: arg.name,
      lastName: arg.surname,
      birthday: arg.datumRodjenja,
      adresa: arg.address,
      tipPutnika: arg.tipputnika
    };

    return this.http.post<any>('http://localhost:52295/api/Registracija/registracijaKorisnika', par, headers);
  }

  izmeniProfil(arg: any): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    let par = {
      username: arg.email,
      password: arg.password,
      name: arg.name,
      lastName: arg.surname,
      birthday: arg.datumRodjenja,
      adresa: arg.address,
      tipPutnika: arg.tipPutnika,
      originalPassword: arg.CurrentPassword
    };

    return this.http.post<any>('http://localhost:52295/api/Registracija/izmeniProfil', par, headers);
  }

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

  getDanasnjiDatum(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getDanasnjiDatum');
  }

  getTipRedVoznje(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getTipRedVoznje');
  }

  getTipDana(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getTipDana');
  }

  getLinijeGradskeRadniDan(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinijeGradskeRadniDan');
  }

  getLinijeGradskeSubota(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinijeGradskeSubota');
  }

  getLinijeGradskeNedelja(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinijeGradskeNedelja');
  }

  getLinijePrigradskeRadniDan(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinijePrigradskeRadniDan');
  }

  getLinijePrigradskeSubota(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinijePrigradskeSubota');
  }

  getLinijePrigradskeNedelja(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinijePrigradskeNedelja');
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
