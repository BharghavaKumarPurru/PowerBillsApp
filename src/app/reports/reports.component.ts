import { Component, OnInit } from '@angular/core';
import { ReportsService } from './reports.service';

@Component({
  selector: 'app-reports',
  templateUrl: './reports.component.html',
  styleUrls: ['./reports.component.css']
})
export class ReportsComponent implements OnInit {
  filters = {
    startDate: '',
    endDate: '',
    minAmount: null,
    maxAmount: null,
    description: ''
  };

  constructor(private reportsService: ReportsService) {}

  ngOnInit(): void {}

  exportToExcel(): void {
    this.reportsService.exportToExcel(this.filters).subscribe(
      (response) => {
        const blob = new Blob([response], { type: 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' });
        const url = window.URL.createObjectURL(blob);
        const anchor = document.createElement('a');
        anchor.href = url;
        anchor.download = 'BillsReport.xlsx';
        anchor.click();
      },
      (error) => {
        console.error('Error exporting to Excel:', error);
      }
    );
  }

  exportToPdf(): void {
    this.reportsService.exportToPdf().subscribe(
      (response) => {
        const blob = new Blob([response], { type: 'application/pdf' });
        const url = window.URL.createObjectURL(blob);
        const anchor = document.createElement('a');
        anchor.href = url;
        anchor.download = 'BillsReport.pdf';
        anchor.click();
      },
      (error) => {
        console.error('Error exporting to PDF:', error);
      }
    );
  }
}
