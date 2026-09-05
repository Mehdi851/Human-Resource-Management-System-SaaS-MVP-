export interface ReportSummary {
  totalEmployees: number;
  presentToday: number;
  pendingLeaves: number;
  payrollAmount: number;
}

export interface DepartmentEmployeeReport {
  department: string;
  employeeCount: number;
}

export interface AttendanceReport {
  date: string;
  present: number;
  absent: number;
  late: number;
  leave: number;
}

export interface LeaveReport {
  leaveType: string;
  count: number;
}

export interface EmployeeReport {
  id: number;
  employeeCode: string;
  employeeName: string;
  department: string;
  designation: string;
  employmentStatus: 'Active' | 'Inactive';
  joiningDate: string;
}

export interface ReportFilter {
  fromDate: string;
  toDate: string;
  department: string;
  reportType: string;
}