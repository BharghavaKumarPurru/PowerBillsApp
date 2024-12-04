import { Component, OnInit } from '@angular/core';
import { BillService } from '../../services/bill.service';

@Component({
  selector: 'app-bill-list',
  templateUrl: './bill-list.component.html',
  styleUrls: ['./bill-list.component.css']
})
export class BillListComponent implements OnInit {
  bills: any[] = [];
  
  // Define filters object
  filters = {
    startDate: '',
    endDate: '',
    minAmount: null,
    maxAmount: null,
    description: '',
  };

  constructor(private billService: BillService) {}

  ngOnInit(): void {
    this.loadBills(); // Load all bills on initialization
  }

  // Apply filters to fetch filtered bills
  applyFilters(): void {
    const params = { ...this.filters };
    this.billService.getFilteredBills(params).subscribe(
      (data) => {
        this.bills = data; // Update the list with filtered data
      },
      (error) => {
        console.error('Error applying filters:', error);
      }
    );
  }

  // Load all bills
  loadBills(): void {
    this.billService.getBills().subscribe(
      (data) => {
        this.bills = data;
      },
      (error) => {
        console.error('Error fetching bills:', error);
      }
    );
  }

  
  // Delete a bill by ID
  deleteBill(billId: number): void {
    if (confirm('Are you sure you want to delete this bill?')) {
      this.billService.deleteBill(billId).subscribe(
        () => {
          console.log(`Bill with ID ${billId} deleted`);
          this.bills = this.bills.filter((bill) => bill.id !== billId); // Update UI after deletion
        },
        (error) => {
          console.error('Error deleting bill:', error);
        }
      );
    }
  }
}
