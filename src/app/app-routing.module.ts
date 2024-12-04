import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { BillListComponent } from './components/bill-list/bill-list.component';
import { BillFormComponent } from './components/bill-form/bill-form.component';
import { ReportsComponent } from './reports/reports.component';
import { ChartsComponent } from './charts/charts.component';



const routes: Routes = [
  { path: '', component: BillListComponent },
  { path: 'add', component: BillFormComponent },
  { path: 'reports', component: ReportsComponent },
  { path: 'edit/:id', component: BillFormComponent },
  { path: 'charts', component: ChartsComponent }, 
  { path: 'etl', loadChildren: () => import('./etl/etl.module').then(m => m.EtlModule) },

];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
