import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class EtlService {
  constructor(private http: HttpClient) {}

  runETL() {
    return this.http.post('https://localhost:7154/api/etl/run', {});
  }
}
