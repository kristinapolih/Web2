
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { FormBuilder, ReactiveFormsModule, FormsModule } from '@angular/forms';  
import { Routes, RouterModule } from '@angular/router';
import { RedVoznjeComponent } from './red-voznje/red-voznje.component';
import { MrezaLinijaComponent } from './mreza-linija/mreza-linija.component';
import { TrenutnaLokacijaVozilaComponent } from './trenutna-lokacija-vozila/trenutna-lokacija-vozila.component';
import { CenovnikComponent } from './cenovnik/cenovnik.component';
import { LoginComponent } from './login/login.component';
import { RegistracijaComponent } from 'src/app/registracija/registracija.component';
import { AppComponent } from './app.component';

const routes: Routes = [
  { path: '', component: RedVoznjeComponent },
  { path: 'red-voznje', component: RedVoznjeComponent },
  { path: 'mreza-linija', component: MrezaLinijaComponent },
  { path: 'trenutna-lokacija-vozila', component: TrenutnaLokacijaVozilaComponent },
  { path: 'cenovnik', component: CenovnikComponent },
  { path: 'prijavite-se', component: LoginComponent },
  { path: 'registracija', component: RegistracijaComponent }
];

@NgModule({
  imports: [
    BrowserModule, 
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot(routes)
  ],
  providers: [],
  bootstrap: [AppComponent],
  exports: [RouterModule]
})
export class AppRoutingModule { }

