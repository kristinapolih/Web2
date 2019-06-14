import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';
import { catchError, map } from 'rxjs/operators';
import { of } from 'rxjs';
import { Karta } from 'src/app/cenovnik/karta';
import { CenovnikHelp } from './cenovnik-admin/cenovnik';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  polasci: any;
  linija: any;

  constructor(private http: HttpClient) { }

  onSubmitSaveChanges(arg:any):Observable<any>
  {
    return this.http.post<any>('http://localhost:52295/api/MrezaLinija/saveStationChanges', arg);
  }

  deleteStationFromRoute(arg:any):Observable<any>
  {
    let par = {
      BrojRute: arg.BrojRute,
      Naziv: arg.DeleteStationRoute,
      ID: arg.IdStation
    };

    return this.http.post<any>('http://localhost:52295/api/MrezaLinija/deleteStationFromRoute', par);
  }

  addStation(arg:any):Observable<any>
  {
    let par = {
      Naziv: arg.Name,
      Adresa: arg.Address,
      string: arg.RouteNumber,
      IDRute: arg.IdRoute,
      X: arg.X,
      Y: arg.Y,
      BrojeviRuta: arg.RouteNumbers,
      brojURuti: arg.NumberInRoute,
    };

    return this.http.post<any>('http://localhost:52295/api/MrezaLinija/addStation', par);
  }

  addLines(arg:any):Observable<any>
  {
    return this.http.post<any>('http://localhost:52295/api/MrezaLinija/addLines', arg);
  }

  getNewRoutes(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/MrezaLinija/getNewRoutes');
  }

  getRoutesAddStation(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/MrezaLinija/getRoutesAddStation');
  }

  getStationsAdmin(): Observable<any>
  {
    return this.http.get<any>('http://localhost:52295/api/MrezaLinija/getStanice');
  }

  dodajKontrolera(arg: any): Observable<any> {

    let par = {
      username: arg.email,
      password: arg.password
    };

    return this.http.post<any>('http://localhost:52295/api/Registracija/dodajKontrolera', par);
  }

  getSlika(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Registracija/getSlika' + `/?id=${id}`);
  }

  ubaciSliku(fileToUpload: File, email: string): Observable<any> {
    const endpoint = 'http://localhost:52295/api/Registracija/ubaciSliku';
    const formData: FormData = new FormData();
    formData.append(email, fileToUpload, fileToUpload.name);
    return this.http.post(endpoint, formData);
  }

  odbijKorisnika(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Registracija/odbijKorisnika' + `/?id=${id}`);
  }

  prihvatiKorisnika(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Registracija/prihvatiKorisnika' + `/?id=${id}`);
  }

  getPutnike(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Registracija/getPutnike');
  }

  getKartu(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Karta/getKartu' + `/?id=${id}`);
  }

  getHub(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Hub/getHub');
  }

  izmeniCenovnik(arg: any, id: number, stamp: string): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    let c: CenovnikHelp = new CenovnikHelp();
    c.ID = id;
    c.DoDatuma = arg.doDatuma;
    c.Stamp = stamp;

    return this.http.post<any>('http://localhost:52295/api/Cenovnik/izmeniCenovnik', c, headers);
  }

  getCenovnikIzmena(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Cenovnik/getCenovnikIzmena' + `/?id=${id}`);
  }

  getCenovnik(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Cenovnik/getCenovnik' + `/?id=${id}`);
  }

  getCenovnike(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Cenovnik/getCenovnike');
  }

  dodajCenovnik(arg: any): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    let par = {
      OdDatuma: arg.odDatuma,
      DoDatuma: arg.doDatuma,
      VremenskaCena: arg.vremenska,
      DnevnaCena: arg.dnevna,
      MesecnaCena: arg.mesecna,
      GodisnjaCena: arg.godisnja
    };
    return this.http.post<any>('http://localhost:52295/api/Cenovnik/dodajCenovnik', par, headers);
  }

  dodajNovuLinijuAdmin(arg: any): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    let par = {
      Dan: arg.danNew,
      Polasci: arg.svipolasciNew,
      ImeRute: arg.naslovNew,
      TipVoznje: arg.redvoznje
    };
    return this.http.post<any>('http://localhost:52295/api/RedVoznje/dodajNovuLinijuAdmin', par, headers);
  }

  obrisiLinijuAdmin(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/obrisiLinijuAdmin' + `/?id=${id}`);
  }

  izmeniDanLinijeAdmin(selectedtipDana: string, idlinije: number, stamp:string): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }
    let par = {
      Dan: selectedtipDana,
      ID: idlinije,
      Stamp: stamp
    };
    return this.http.post<any>('http://localhost:52295/api/RedVoznje/izmeniDanLinijeAdmin', par, headers);
  }

  izmeniImeLinijeAdmin(line: string, idlinije: number, stamp:string): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    let par = {
      ImeRute: line,
      ID: idlinije,
      Stamp: stamp
    };
    return this.http.post<any>('http://localhost:52295/api/RedVoznje/izmeniImeLinijeAdmin', par, headers);
  }

  izmeniPolaskeAdmin(arg: any, idlinije: number, stamp:string): Observable<any> {
    let headers = {
      headers: new HttpHeaders({
        'Content-Type': 'application/json'
      })
    }

    let par = {
      Polasci: arg.svipolasci,
      ID: idlinije,
      Stamp: stamp
    };
    return this.http.post<any>('http://localhost:52295/api/RedVoznje/izmeniPolaskeAdmin', par, headers);
  }

  getLinijuListAdmin(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinijuListAdmin' + `/?id=${id}`);
  }

  getLinijuAdmin(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinijuAdmin' + `/?id=${id}`);
  }

  getLinijeAdmin(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/RedVoznje/getLinijeAdmin');
  }

  getLinijeGradske(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/MrezaLinija/getLinijeGradske');
  }

  getLinijePrigradske(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/MrezaLinija/getLinijePrigradske');
  }

  getLiniju(id: number): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/MrezaLinija/getLiniju' + `/?id=${id}`);
  }

  obrisiKartu(u: any): Observable<any> {
    return this.http.post<any>('http://localhost:52295/api/Karta/obrisiKartu', u);
  }

  getKarte(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Karta/getKarte');
  }

  getCene(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Karta/getCene');
  }

  getKoeficijente(): Observable<any> {
    return this.http.get<any>('http://localhost:52295/api/Karta/getKoeficijente');
  }

  kupiKartu(tipKorisnika: string, tipKarte: string, cena: number): Observable<any> {
    let karta: Karta = new Karta();
    karta.tipKorisnika = tipKorisnika;
    karta.tipKarte = tipKarte;
    karta.cena = cena;
    return this.http.post<any>('http://localhost:52295/api/Karta/kupiKartu', karta);
  }

  getProfil(): Observable<any> {
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

  getPolasci() {
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
