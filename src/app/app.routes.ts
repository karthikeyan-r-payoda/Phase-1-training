import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AdminComponent } from './components/admin/admin.component';
import { StaffComponent } from './components/staff/staff.component';
import { HousekeeperComponent } from './components/housekeeper/housekeeper.component';
import { NotAuthorizedComponent } from './components/not-authorized/not-authorized.component';
import { authGuard } from './guards/auth.guard';
import { AddUserComponent } from './components/user/add-user/add-user.component';
import { UserListComponent } from './components/user/user-list/user-list.component';
import { EditUserComponent } from './components/user/edit-user/edit-user.component';
import { AddRoomComponent } from './components/rooms/add-room/add-room.component';
import { RoomListComponent } from './components/rooms/room-list/room-list.component';
import { EditRoomComponent } from './components/rooms/edit-room/edit-room.component';
import { GenerateReportComponent } from './report/generate-report/generate-report.component';
import { Component } from '@angular/core';
import { AllReportsComponent } from './report/all-reports/all-reports.component';
import { AddGuestComponent } from './components/guest/add-guest/add-guest.component';
import { EditGuestComponent } from './components/guest/edit-guest/edit-guest.component';
import { GuestListComponent } from './components/guest/guest-list/guest-list.component';
import { AddInventoryComponent } from './components/inventory/add-inventory/add-inventory.component';
import { EditInventoryComponent } from './components/inventory/edit-inventory/edit-inventory.component';
import { InventoryListComponent } from './components/inventory/inventory-list/inventory-list.component';
import { LowStockComponent } from './components/inventory/low-stock/low-stock.component';
import { AddReservationComponent } from './components/reservation/add-reservation/add-reservation.component';
import { ReservationListComponent } from './components/reservation/reservation-list/reservation-list.component';
import { RoomListComponent1 } from './components/booking/rooms/rooms.component';
import { PaymentComponent } from './components/payment/payment.component';
import { MaintenanceListComponent } from './components/maintenance/maintenance.component';
import { EditMaintenanceComponent } from './components/maintenance/edit-maintenance/edit-maintenance.component';
import { AddMaintenanceComponent } from './components/maintenance/add-maintenance/add-maintenance.component';
import { HousekeepingListComponentforAdmin } from './components/housekeeping/housekeeping-list/housekeeping-list.component';
import { AddHousekeepingComponent } from './components/housekeeping/add-housekeeping/add-housekeeping.component';
import { EditHousekeepingComponent } from './components/housekeeping/edit-housekeeping/edit-housekeeping.component';

export const routes: Routes = [
  { path: '', component: LoginComponent },
  { 
  path: 'admin', 
  component: AdminComponent, 
  canActivate: [authGuard], 
  data: { roles: ['admin'] } 
},
{ 
  path: 'staff', 
  component: StaffComponent, 
  canActivate: [authGuard], 
  data: { roles: ['staff'] } 
},
{ 
  path: 'housekeeper', 
  component: HousekeeperComponent, 
  canActivate: [authGuard], 
  data: { roles: ['housekeeperstaff'] } 
},
 { path: 'admin/add-user', component: AddUserComponent },
 { path: 'admin/user-list', component: UserListComponent },
 { path:'admin/edit-user',component:EditUserComponent},

  { path: 'admin/rooms/add', component: AddRoomComponent },
  { path: 'admin/rooms/list', component: RoomListComponent },
  { path: 'admin/rooms/edit/:id', component: EditRoomComponent },

  {path:'reports/generate',component:GenerateReportComponent},
  {path:'reports/list',component:AllReportsComponent},

  {path:'guest/list',component:GuestListComponent},
  {path:'guest/add',component:AddGuestComponent},
  {path:'guest/edit',component:EditGuestComponent},

  { path: 'inventory/add', component: AddInventoryComponent },
  { path: 'inventory/list', component: InventoryListComponent },
  { path: 'inventory/edit/:id', component: EditInventoryComponent },
   { path: 'inventory/low-stock', component: LowStockComponent },
   { path: 'admin/inventory/low-stock', component: LowStockComponent },

{ path: 'rooms/list', component: RoomListComponent1 },
   { path: 'reservation/add/:roomId', component: AddReservationComponent },
     { path: 'reservation/list', component: ReservationListComponent },

{ path: 'maintenance/list', component: MaintenanceListComponent },
{ path: 'maintenance/add', component: AddMaintenanceComponent },
{ path: 'maintenance/edit/:id', component: EditMaintenanceComponent },

{ path: 'housekeeping/list', component: HousekeepingListComponentforAdmin },
{ path: 'housekeeping/add', component: AddHousekeepingComponent },
{ path: 'housekeeping/update', component: EditHousekeepingComponent },


{path:'payment',component:PaymentComponent},
  { path: 'not-authorized', component: NotAuthorizedComponent }
];


