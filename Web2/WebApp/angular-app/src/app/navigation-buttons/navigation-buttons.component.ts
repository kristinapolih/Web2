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

}
