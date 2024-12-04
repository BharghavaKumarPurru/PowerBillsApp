import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EtlComponent } from './etl/etl.component';
import { EtlService } from './etl.service';
import { EtlRoutingModule } from './etl-routing.module';

@NgModule({
  declarations: [EtlComponent],
  imports: [
    CommonModule,
    EtlRoutingModule, // Include the routing module
  ],
  providers: [EtlService], // Provide the ETL service
})
export class EtlModule {}
