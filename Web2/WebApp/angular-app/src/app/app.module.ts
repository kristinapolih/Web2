import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule,FormBuilder } from '@angular/forms';

import { HttpClientModule } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavigationButtonsComponent } from './navigation-buttons/navigation-buttons.component';
import { RedVoznjeComponent } from './red-voznje/red-voznje.component';

import { AccordionModule } from 'ngx-bootstrap/accordion';
import { MrezaLinijaComponent } from './mreza-linija/mreza-linija.component';
import { TrenutnaLokacijaVozilaComponent } from './trenutna-lokacija-vozila/trenutna-lokacija-vozila.component';
import { CenovnikComponent } from './cenovnik/cenovnik.component';
import { LoginComponent } from './login/login.component';
import { RegistracijaComponent } from './registracija/registracija.component';
import { PromeniVidiProfilComponent } from './promeni-vidi-profil/promeni-vidi-profil.component';


@NgModule({
  declarations: [
    AppComponent,
    NavigationButtonsComponent,
    RedVoznjeComponent,
    MrezaLinijaComponent,
    TrenutnaLokacijaVozilaComponent,
    CenovnikComponent,
    LoginComponent,
    RegistracijaComponent,
    PromeniVidiProfilComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    AccordionModule.forRoot()
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
