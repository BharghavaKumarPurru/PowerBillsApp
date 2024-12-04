import { Component, OnInit } from '@angular/core';
import { BillService } from '../services/bill.service';
import Chart from 'chart.js/auto';

@Component({
  selector: 'app-charts',
  templateUrl: './charts.component.html',
  styleUrls: ['./charts.component.css']
})
export class ChartsComponent implements OnInit {
  constructor(private billService: BillService) {}

  ngOnInit(): void {
    this.loadChart();
  }

  loadChart(): void {
    this.billService.getBills().subscribe((bills: any[]) => {
      // Group bills by description and calculate the average amount for each group
      const groupedData = bills.reduce((acc, bill) => {
        if (!acc[bill.description]) {
          acc[bill.description] = { total: 0, count: 0 };
        }
        acc[bill.description].total += bill.amount;
        acc[bill.description].count += 1;
        return acc;
      }, {});
  
      // Extract labels and data for the chart
      const labels = Object.keys(groupedData);
      const data = labels.map(label => (groupedData[label].total / groupedData[label].count).toFixed(2));
  
      // Render the chart
      new Chart('billsChart', {
        type: 'bar',
        data: {
          labels,
          datasets: [
            {
              label: 'Average Bill Amounts',
              data,
              backgroundColor: 'rgba(54, 162, 235, 0.2)',
              borderColor: 'rgba(54, 162, 235, 1)',
              borderWidth: 1,
            },
          ],
        },
        options: {
          responsive: true,
          scales: {
            y: {
              beginAtZero: true,
            },
          },
        },
      });
    });
  }
}  