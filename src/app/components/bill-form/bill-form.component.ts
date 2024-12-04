import { Component } from '@angular/core';
import { BillService } from '../../services/bill.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-bill-form',
  templateUrl: './bill-form.component.html',
  styleUrls: ['./bill-form.component.css'],
})
export class BillFormComponent {
  bill: any = {
    billNumber: '',
    description: '',
    amount: 0,
    createdDate: '',
  };

  constructor(private billService: BillService, private router: Router) {}

  saveBill(): void {
    this.billService.addBill(this.bill).subscribe(
      (response) => {
        console.log('Bill added:', response);
        this.router.navigate(['/']);
      },
      (error) => {
        console.error('Error adding bill:', error);
      }
    );
  }
}
