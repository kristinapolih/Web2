import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule,FormBuilder } from '@angular/forms';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NavigationButtonsComponent } from './navigation-buttons/navigation-buttons.component';
import { RedVoznjeComponent } from './red-voznje/red-voznje.component';

import { AccordionModule } from 'ngx-bootstrap/accordion';
import { MrezaLinijaComponent } from './mreza-linija/mreza-linija.component';
import { CenovnikComponent } from './cenovnik/cenovnik.component';
import { LoginComponent } from './login/login.component';
import { RegistracijaComponent } from './registracija/registracija.component';
import { PromeniVidiProfilComponent } from './promeni-vidi-profil/promeni-vidi-profil.component';
import { ProveriKarteComponent } from './proveri-karte/proveri-karte.component';
import { RedVoznjePrikaziLinijuComponent } from './red-voznje-prikazi-liniju/red-voznje-prikazi-liniju.component';
import { JwtInterceptor } from 'src/app/auth/jwt-interceptor';
import { MapaComponent } from './mapa/mapa.component';
import { AgmCoreModule } from '@agm/core';
import { RedVoznjeAdminComponent } from './red-voznje-admin/red-voznje-admin.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { CenovnikAdminComponent } from './cenovnik-admin/cenovnik-admin.component';
import { VerifikacijaComponent } from './verifikacija/verifikacija.component';
import { MrezaLinijaLinijaAdminComponent } from './mreza-linija-linija-admin/mreza-linija-linija-admin.component';
import { MrezaLinijaStanicaAdminComponent } from './mreza-linija-stanica-admin/mreza-linija-stanica-admin.component';
import { DodajKontroleraAdminComponent } from './dodaj-kontrolera-admin/dodaj-kontrolera-admin.component';

@NgModule({
  declarations: [
    AppComponent,
    NavigationButtonsComponent,
    RedVoznjeComponent,
    MrezaLinijaComponent,
    CenovnikComponent,
    LoginComponent,
    RegistracijaComponent,
    PromeniVidiProfilComponent,
    ProveriKarteComponent,
    RedVoznjePrikaziLinijuComponent,
    MapaComponent,
    RedVoznjeAdminComponent,
    CenovnikAdminComponent,
    VerifikacijaComponent,
    MrezaLinijaLinijaAdminComponent,
    MrezaLinijaStanicaAdminComponent,
    DodajKontroleraAdminComponent
  ],
  imports: [
    BrowserModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule,
    HttpClientModule,
    BrowserAnimationsModule,
    AccordionModule.forRoot(),
    AgmCoreModule.forRoot({apiKey: 'AIzaSyDnihJyw_34z5S1KZXp90pfTGAqhFszNJk'})
  ],
  providers: [
    {
      provide: HTTP_INTERCEPTORS,
      useClass: JwtInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
