
import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { FormBuilder, ReactiveFormsModule, FormsModule } from '@angular/forms';
import { Routes, RouterModule } from '@angular/router';

import { AuthGuard } from './auth/auth.guard';
import { RedVoznjeComponent } from './red-voznje/red-voznje.component';
import { MrezaLinijaComponent } from './mreza-linija/mreza-linija.component';
import { CenovnikComponent } from './cenovnik/cenovnik.component';
import { LoginComponent } from './login/login.component';
import { RegistracijaComponent } from 'src/app/registracija/registracija.component';
import { AppComponent } from './app.component';
import { PromeniVidiProfilComponent } from './promeni-vidi-profil/promeni-vidi-profil.component';
import { ProveriKarteComponent } from 'src/app/proveri-karte/proveri-karte.component';
import { ControllerGuard } from 'src/app/auth/controller.guard';
import { AppUserGuard } from 'src/app/auth/appUser.guard';
import { AuthNotGuard } from 'src/app/auth/authNot.guard';
import { RedVoznjePrikaziLinijuComponent } from 'src/app/red-voznje-prikazi-liniju/red-voznje-prikazi-liniju.component';
import { AdminGuard } from './auth/admin.guard';
import { RedVoznjeAdminComponent } from './red-voznje-admin/red-voznje-admin.component';

const routes: Routes = [
  {
    path: 'red-voznje',
    component: RedVoznjeComponent,
    children: [{
      path: 'linija',
      component: RedVoznjePrikaziLinijuComponent
    }]
  },
  {
    path: 'mreza-linija',
    component: MrezaLinijaComponent
  },
  {
    path: 'cenovnik',
    component: CenovnikComponent
  },
  {
    path: 'prijavite-se',
    component: LoginComponent,
    canActivate: [AuthNotGuard]
  },
  {
    path: 'registracija',
    component: RegistracijaComponent,
    canActivate: [AuthNotGuard]
  },
  {
    path: 'promeni-vidi-profil',
    component: PromeniVidiProfilComponent,
    canActivate: [AuthGuard, AppUserGuard]
  },
  {
    path: 'proveri-karte',
    component: ProveriKarteComponent,
    canActivate: [AuthGuard, ControllerGuard]
  },

  {
    path: 'red-voznje-admin',
    component: RedVoznjeAdminComponent,
    canActivate: [AuthGuard, AdminGuard]
  }
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

