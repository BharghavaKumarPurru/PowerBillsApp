import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ReportsService {
  private baseUrl = 'http://localhost:7154/api/bills'; // Update this URL according to your backend

  constructor(private http: HttpClient) {}

  exportToExcel(filters: any): Observable<Blob> {
    const params: any = { ...filters };
    return this.http.get(`${this.baseUrl}/report/excel`, {
      params,
      responseType: 'blob'
    });
  }
  getAllBills(): Observable<any[]> {
    return this.http.get<any[]>('http://localhost:7154/api/bills'); // Update the URL as per your backend
  }
  

  exportToPdf(): Observable<Blob> {
    return this.http.get(`${this.baseUrl}/report/pdf`, {
      responseType: 'blob'
    });
  }
}
