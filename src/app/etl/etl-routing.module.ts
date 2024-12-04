import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { EtlComponent } from './etl/etl.component';

const routes: Routes = [
  { path: '', component: EtlComponent }, // Default route for the ETL module
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule],
})
export class EtlRoutingModule {}
