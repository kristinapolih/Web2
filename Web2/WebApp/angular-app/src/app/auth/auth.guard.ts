import { Injectable } from '@angular/core';
import {
  CanActivate, Router,
  ActivatedRouteSnapshot,
  RouterStateSnapshot,
  CanActivateChild,
} from '@angular/router';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class AuthGuard implements CanActivate, CanActivateChild {
  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    if (localStorage.login) {
      console.log(state.url);
      console.log(localStorage.role);
      if (localStorage.role == "Admin" && (state.url == "/red-voznje" ||
      state.url == "/mreza-linija" ||
        state.url == "/stanice" ||
        state.url == "/cenovnik" ||
        state.url == "/linije")) {
        return true;
      }
      else if (localStorage.role == "AppUser" && (state.url == "/red-voznje" ||
      state.url == "/mreza-linija" ||
        //state.url == "/karte" ||
        state.url == "/cenovnik" ||
        state.url == "/promeni-vidi-profil")) {
        return true;
      }
      else if (localStorage.role == "Controller" && (state.url == "/verifikacija-putnika" ||
        state.url == "/karte")) {
        return true;
      }
      else {
        console.error("Access denied");
        if (localStorage.role == "Admin") {
          this.router.navigate(['/red-voznje']);
        }
        else if (localStorage.role == "AppUser") {
          this.router.navigate(['/red-voznje']);
        }
        else if (localStorage.role == "Controller") {
          this.router.navigate(['/karte']);
        }
        return false;
      }
    }
    else {
      console.log(localStorage.login);
      console.error("Access denied");
      this.router.navigate(['/']);
      return false;
    }
  }

  canActivateChild(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean {
    return this.canActivate(route, state);
  }

}
