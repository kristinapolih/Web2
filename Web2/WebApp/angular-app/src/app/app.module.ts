import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { ReactiveFormsModule, FormsModule,FormBuilder } from '@angular/forms';

import { PageNotFoundComponent }   from './page-not-found/page-not-found.component';
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


@NgModule({
  declarations: [
    AppComponent,
    PageNotFoundComponent,
    NavigationButtonsComponent,
    RedVoznjeComponent,
    MrezaLinijaComponent,
    TrenutnaLokacijaVozilaComponent,
    CenovnikComponent,
    LoginComponent,
    RegistracijaComponent
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
