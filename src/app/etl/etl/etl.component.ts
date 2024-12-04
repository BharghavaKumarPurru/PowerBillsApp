import { Component } from '@angular/core';
import { EtlService } from '../etl.service';

@Component({
  selector: 'app-etl',
  templateUrl: './etl.component.html',
})
export class EtlComponent {
  message: string = '';

  constructor(private etlService: EtlService) {}

  runETL() {
    this.etlService.runETL().subscribe({
      next: (response: any) => {
        this.message = response.message;
      },
      error: (err) => {
        console.error('Error running ETL:', err);
        this.message = 'Failed to run ETL process.';
      },
    });
  }
}
