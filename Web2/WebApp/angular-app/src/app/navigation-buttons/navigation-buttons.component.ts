import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/auth/auth.service';

@Component({
  selector: 'app-navigation-buttons',
  templateUrl: './navigation-buttons.component.html',
  styleUrls: ['./navigation-buttons.component.css']
})
export class NavigationButtonsComponent implements OnInit {

appTitle: string = 'BusNS';

  constructor(public authService: AuthService) { }

  ngOnInit() {
  }

  get isLogged ()
  {
    return this.authService.isLoggedIn;
  }

  logout(): any
  {
    localStorage.removeItem('jwt');
    localStorage.removeItem('role');
    localStorage.removeItem('login');
  }

  isLogin(): boolean
{
  if(localStorage.login)
  {
    return true;
  }
  else
  {
    return false;
  }
}

isAdmin()
{
  if(localStorage.role){
      if(localStorage.role == "Admin")
      {
        return true;
      }
      else
      {
        return false;
      }
  }
  return false;
}

isController()
{
  if(localStorage.role){
    if(localStorage.role == "Controller")
    {
      return true;
    }
    else
    {
      return false;
    }
  }
  return false;
}

isUser()
{
  if(localStorage.role)
  {
    if(localStorage.role == "AppUser")
    {
      return true;
    }
    else
    {
      return false;
    }
  }
  return true;
}

isUserProfile()
{
  if(localStorage.role)
  {
    if(localStorage.role == "AppUser")
    {
      return true;
    }
    else
    {
      return false;
    }
  }
  return false;
}

}
