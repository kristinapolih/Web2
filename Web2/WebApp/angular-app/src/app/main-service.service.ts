import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class MainServiceService {

  constructor(private http: HttpClient) { }

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
