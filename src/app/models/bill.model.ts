// src/app/models/bill.model.ts
export interface Bill {
    id: number; // Bill ID
    billNumber: string; // Bill Number
    description: string; // Description of the bill
    amount: number; // Amount of the bill
    createdDate: Date; // Date of creation
  }
  