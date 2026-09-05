import {
  AttendanceReport,
  DepartmentEmployeeReport,
  EmployeeReport,
  LeaveReport,
  ReportSummary
} from '../models/report.model';

export const REPORT_SUMMARY: ReportSummary = {
  totalEmployees: 128,
  presentToday: 112,
  pendingLeaves: 7,
  payrollAmount: 2850000
};

export const DEPARTMENT_EMPLOYEE_REPORT: DepartmentEmployeeReport[] = [
  {
    department: 'Engineering',
    employeeCount: 42
  },
  {
    department: 'Human Resources',
    employeeCount: 12
  },
  {
    department: 'Finance',
    employeeCount: 18
  },
  {
    department: 'Sales',
    employeeCount: 28
  },
  {
    department: 'Operations',
    employeeCount: 20
  },
  {
    department: 'Administration',
    employeeCount: 8
  }
];

export const ATTENDANCE_REPORT: AttendanceReport[] = [
  {
    date: '2026-09-01',
    present: 108,
    absent: 8,
    late: 7,
    leave: 5
  },
  {
    date: '2026-09-02',
    present: 111,
    absent: 6,
    late: 5,
    leave: 6
  },
  {
    date: '2026-09-03',
    present: 115,
    absent: 5,
    late: 4,
    leave: 4
  },
  {
    date: '2026-09-04',
    present: 112,
    absent: 7,
    late: 6,
    leave: 3
  },
  {
    date: '2026-09-05',
    present: 112,
    absent: 6,
    late: 5,
    leave: 5
  }
];

export const LEAVE_REPORT: LeaveReport[] = [
  {
    leaveType: 'Annual Leave',
    count: 18
  },
  {
    leaveType: 'Sick Leave',
    count: 11
  },
  {
    leaveType: 'Casual Leave',
    count: 9
  },
  {
    leaveType: 'Unpaid Leave',
    count: 4
  }
];

export const EMPLOYEE_REPORT: EmployeeReport[] = [
  {
    id: 1,
    employeeCode: 'EMP-001',
    employeeName: 'Ahmed Khan',
    department: 'Engineering',
    designation: 'Senior Software Engineer',
    employmentStatus: 'Active',
    joiningDate: '2024-02-15'
  },
  {
    id: 2,
    employeeCode: 'EMP-002',
    employeeName: 'Sara Ahmed',
    department: 'Human Resources',
    designation: 'HR Executive',
    employmentStatus: 'Active',
    joiningDate: '2023-08-10'
  },
  {
    id: 3,
    employeeCode: 'EMP-003',
    employeeName: 'Usman Ali',
    department: 'Finance',
    designation: 'Accountant',
    employmentStatus: 'Active',
    joiningDate: '2022-11-21'
  },
  {
    id: 4,
    employeeCode: 'EMP-004',
    employeeName: 'Fatima Noor',
    department: 'Sales',
    designation: 'Sales Executive',
    employmentStatus: 'Active',
    joiningDate: '2025-01-06'
  },
  {
    id: 5,
    employeeCode: 'EMP-005',
    employeeName: 'Hassan Raza',
    department: 'Operations',
    designation: 'Operations Officer',
    employmentStatus: 'Inactive',
    joiningDate: '2021-06-14'
  }
];