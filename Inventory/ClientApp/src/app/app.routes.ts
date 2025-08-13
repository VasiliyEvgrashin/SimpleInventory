import { Routes } from '@angular/router';
import { MainContainerComponent } from './maincontainer/maincontainer.component';
import { PageNotFoundComponent } from './notfound/notfound.component';
import { WarehouseComponent } from './maincontainer/warehouse/warehouse.component';
import { BalanceComponent } from './maincontainer/warehouse/balance/balance.component';
import { ReceiptsComponent } from './maincontainer/warehouse/receipts/receipts.component';
import { ShipmentsComponent } from './maincontainer/warehouse/shipments/shipments.component';
import { ReferencebooksComponent } from './maincontainer/referencebooks/referencebooks.component';
import { ListingComponent } from './maincontainer/referencebooks/generic/listing/listing.component';
import { Constants } from './constants';
import { DetailComponent } from './maincontainer/referencebooks/generic/detail/detail.component';
import { ReceiptFormComponent } from './maincontainer/warehouse/receipts/receiptform/receiptform.component';
import { ShipmentFormComponent } from './maincontainer/warehouse/shipments/shipmentform/shipmentform.component';

export const routes: Routes = [
  { path: '', redirectTo: 'main/warehouse/balance', pathMatch: 'full' },
  { path: 'main', component: MainContainerComponent, children: [
    { path: 'warehouse', component: WarehouseComponent, children: [
      { path: '', redirectTo: 'balance', pathMatch: 'full' },
      { path: 'balance', component: BalanceComponent },
      { path: 'receipts', component: ReceiptsComponent },
      { path: 'receipts/:id', component: ReceiptFormComponent },
      { path: 'shipments', component: ShipmentsComponent },
      { path: 'shipments/:id', component: ShipmentFormComponent }
    ] },
    { path: 'referencebooks', component: ReferencebooksComponent, children: [
      { path: '', redirectTo: 'clients', pathMatch: 'full' },
      { path: 'clients', component: ListingComponent, data: { mtype: Constants.clients_data_type_url } },
      { path: 'clients/:id', component: DetailComponent, data: { mtype: Constants.clients_data_type_url } },
      { path: 'unitsofmeasurement', component: ListingComponent, data: { mtype: Constants.unitsofmeasurement_data_type_url } },
      { path: 'unitsofmeasurement/:id', component: DetailComponent, data: { mtype: Constants.unitsofmeasurement_data_type_url } },
      { path: 'resources', component: ListingComponent, data: { mtype: Constants.resources_data_type_url } },
      { path: 'resources/:id', component: DetailComponent, data: { mtype: Constants.resources_data_type_url } },
    ] },
    { path: 'pnf', component: PageNotFoundComponent }
  ] },
  { path: '**', redirectTo: 'main/pnf', pathMatch: 'full' }
];
