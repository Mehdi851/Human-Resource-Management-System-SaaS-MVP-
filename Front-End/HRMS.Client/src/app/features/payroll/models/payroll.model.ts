export type PayrollStatus =
  | 'Draft'
  | 'Processing'
  | 'Processed'
  | 'Paid'
  | 'On Hold';

export interface PayrollModel {
  id: number;
  employeeId: string;
  employeeName: string;
  department: string;
  period: string;

  basicSalary: number;
  allowances: number;
  deductions: number;
  grossSalary: number;
  netSalary: number;

  status: PayrollStatus;
  remarks?: string;
}