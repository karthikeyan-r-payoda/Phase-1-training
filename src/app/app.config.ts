import { ApplicationConfig } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient } from '@angular/common/http';

import { LoginComponent } from './components/login/login.component';
import { authGuard } from './guards/auth.guard';

import { AdminComponent } from './components/admin/admin.component';
import { StaffComponent } from './components/staff/staff.component';
import { HousekeeperComponent } from './components/housekeeper/housekeeper.component';
import { NotAuthorizedComponent } from './components/not-authorized/not-authorized.component';

export const routes = [
  { path: '', component: LoginComponent },
  { path: 'admin', component: AdminComponent, canActivate: [authGuard] },
  { path: 'staff', component: StaffComponent, canActivate: [authGuard] },
  { path: 'housekeeper', component: HousekeeperComponent, canActivate: [authGuard] },
  { path: 'not-authorized', component: NotAuthorizedComponent }

];
export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes),
    provideHttpClient()
  ]
};
