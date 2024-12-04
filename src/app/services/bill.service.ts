import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Bill } from '../models/bill.model'; // Adjust the path as needed


@Injectable({
  providedIn: 'root',
})
export class BillService {
  private apiUrl = 'https://localhost:7154/api/bills'; // Updated API URL

  constructor(private http: HttpClient) {}

  getBills(): Observable<any> {
    return this.http.get(this.apiUrl);
  }

  addBill(bill: any): Observable<any> {
    return this.http.post(this.apiUrl, bill);
  }

  deleteBill(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }
  downloadReport() {
    return this.http.get(`${this.apiUrl}/report`, {
      responseType: 'blob', // Expect a binary file (Excel)
    });
  }
  getFilteredBills(filters: any): Observable<any[]> {
    const params = new HttpParams({
      fromObject: {
        startDate: filters.startDate,
        endDate: filters.endDate,
        minAmount: filters.minAmount,
        maxAmount: filters.maxAmount,
        description: filters.description,
      },
    });
  
    return this.http.get<any[]>('https://localhost:7154/api/bills/filter', { params });
  }
   
}

