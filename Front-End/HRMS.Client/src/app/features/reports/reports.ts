import { DatePipe } from '@angular/common';
import { Component, computed, signal } from '@angular/core';

import {
  ATTENDANCE_REPORT,
  DEPARTMENT_EMPLOYEE_REPORT,
  EMPLOYEE_REPORT,
  LEAVE_REPORT,
  REPORT_SUMMARY
} from './data/report.mock';

@Component({
  selector: 'app-reports',
  standalone: true,
  imports: [ DatePipe],
  templateUrl: './reports.html',
  styleUrl: './reports.scss'
})
export class Reports {

  readonly summary = signal(REPORT_SUMMARY);

  readonly departmentReports = signal(DEPARTMENT_EMPLOYEE_REPORT);

  readonly attendanceReports = signal(ATTENDANCE_REPORT);

  readonly leaveReports = signal(LEAVE_REPORT);

  readonly employeeReports = signal(EMPLOYEE_REPORT);

  readonly selectedReport = signal('Overview');

  readonly searchTerm = signal('');

  readonly selectedDepartment = signal('all');

  readonly fromDate = signal('2026-09-01');

  readonly toDate = signal('2026-09-05');

  readonly isLoading = signal(false);

  readonly exportMessage = signal('');

  readonly filteredEmployees = computed(() => {

    const search = this.searchTerm().trim().toLowerCase();

    const department = this.selectedDepartment();

    return this.employeeReports().filter(employee => {

      const matchesSearch =
        !search ||
        employee.employeeName.toLowerCase().includes(search) ||
        employee.employeeCode.toLowerCase().includes(search) ||
        employee.department.toLowerCase().includes(search) ||
        employee.designation.toLowerCase().includes(search);

      const matchesDepartment =
        department === 'all' ||
        employee.department === department;

      return matchesSearch && matchesDepartment;
    });
  });


  readonly activeEmployees = computed(() =>
    this.employeeReports().filter(
      employee => employee.employmentStatus === 'Active'
    ).length
  );


  readonly totalLeaveRequests = computed(() =>
    this.leaveReports().reduce(
      (total, leave) => total + leave.count,
      0
    )
  );


  readonly totalDepartmentEmployees = computed(() =>
    this.departmentReports().reduce(
      (total, department) => total + department.employeeCount,
      0
    )
  );


  readonly totalAttendanceRecords = computed(() =>
    this.attendanceReports().reduce(
      (total, record) => total + record.present,
      0
    )
  );


  readonly averageAttendance = computed(() => {

    const records = this.attendanceReports();

    if (!records.length) {
      return 0;
    }

    const total = records.reduce(
      (sum, record) =>
        sum + record.present + record.absent + record.leave,
      0
    );

    const present = records.reduce(
      (sum, record) => sum + record.present,
      0
    );

    return total ? (present / total) * 100 : 0;
  });


 setReportType(type: string): void {
  this.selectedReport.set(type);

  setTimeout(() => {
    document
      .getElementById('detailedReports')
      ?.scrollIntoView({
        behavior: 'smooth',
        block: 'start'
      });
  });
}

  updateSearch(value: string): void {
    this.searchTerm.set(value);
  }


  updateDepartment(value: string): void {
    this.selectedDepartment.set(value);
  }


  updateFromDate(value: string): void {
    this.fromDate.set(value);
  }


  updateToDate(value: string): void {
    this.toDate.set(value);
  }


  resetFilters(): void {

    this.fromDate.set('2026-09-01');

    this.toDate.set('2026-09-05');

    this.selectedDepartment.set('all');

    this.searchTerm.set('');

    this.selectedReport.set('Overview');

    this.exportMessage.set('');
  }


  formatCurrency(value: number): string {

    return new Intl.NumberFormat('en-PK', {
      style: 'currency',
      currency: 'PKR',
      maximumFractionDigits: 0
    }).format(value);
  }


  formatDate(date: string): string {

    return new Intl.DateTimeFormat('en-GB', {
      day: '2-digit',
      month: 'short',
      year: 'numeric'
    }).format(new Date(date));
  }


  getDepartmentPercentage(employeeCount: number): number {

    const total = this.totalDepartmentEmployees();

    if (!total) {
      return 0;
    }

    return (employeeCount / total) * 100;
  }


  getAttendancePercentage(
    present: number,
    absent: number,
    leave: number
  ): number {

    const total = present + absent + leave;

    if (!total) {
      return 0;
    }

    return (present / total) * 100;
  }


  getLeavePercentage(count: number): number {

    const total = this.totalLeaveRequests();

    if (!total) {
      return 0;
    }

    return (count / total) * 100;
  }


  getAttendanceBarHeight(value: number): number {

    const records = this.attendanceReports();

    if (!records.length) {
      return 0;
    }

    const maximum = Math.max(
      ...records.map(record => record.present)
    );

    if (!maximum) {
      return 0;
    }

    return (value / maximum) * 100;
  }


  getAttendanceStatus(
    present: number,
    absent: number,
    leave: number
  ): string {

    if (leave > 0 && present === 0) {
      return 'Leave';
    }

    if (absent > present) {
      return 'Absent';
    }

    return 'Present';
  }


  exportReport(): void {

    const reportType = this.selectedReport();

    let csv = '';

    if (reportType === 'Employees') {
      csv = this.buildEmployeeCsv();
    }

    if (reportType === 'Attendance') {
      csv = this.buildAttendanceCsv();
    }

    if (reportType === 'Leave') {
      csv = this.buildLeaveCsv();
    }

    if (reportType === 'Payroll') {
      this.exportMessage.set(
        'Payroll export will be available after API integration.'
      );

      return;
    }

    if (!csv) {
      this.exportMessage.set(
        'Select a detailed report before exporting.'
      );

      return;
    }

    const blob = new Blob(
      [csv],
      { type: 'text/csv;charset=utf-8;' }
    );

    const url = URL.createObjectURL(blob);

    const link = document.createElement('a');

    link.href = url;

    link.download =
      `${reportType.toLowerCase()}-report.csv`;

    link.click();

    URL.revokeObjectURL(url);

    this.exportMessage.set(
      `${reportType} report exported successfully.`
    );
  }


  printReport(): void {

    window.print();
  }


  private buildEmployeeCsv(): string {

    const headers = [
      'Employee Code',
      'Employee Name',
      'Department',
      'Designation',
      'Joining Date',
      'Status'
    ];

    const rows = this.filteredEmployees().map(employee => [
      employee.employeeCode,
      employee.employeeName,
      employee.department,
      employee.designation,
      employee.joiningDate,
      employee.employmentStatus
    ]);

    return this.convertToCsv(headers, rows);
  }


  private buildAttendanceCsv(): string {

    const headers = [
      'Date',
      'Present',
      'Absent',
      'Late',
      'Leave',
      'Attendance Rate'
    ];

    const rows = this.attendanceReports().map(record => [
      record.date,
      record.present,
      record.absent,
      record.late,
      record.leave,
      `${this.getAttendancePercentage(
        record.present,
        record.absent,
        record.leave
      ).toFixed(1)}%`
    ]);

    return this.convertToCsv(headers, rows);
  }


  private buildLeaveCsv(): string {

    const headers = [
      'Leave Type',
      'Requests',
      'Distribution'
    ];

    const rows = this.leaveReports().map(leave => [
      leave.leaveType,
      leave.count,
      `${this.getLeavePercentage(
        leave.count
      ).toFixed(1)}%`
    ]);

    return this.convertToCsv(headers, rows);
  }


  private convertToCsv(
    headers: string[],
    rows: unknown[][]
  ): string {

    const escapeValue = (value: unknown): string => {

      const text = String(value ?? '');

      return `"${text.replace(/"/g, '""')}"`;
    };

    return [
      headers.map(escapeValue).join(','),
      ...rows.map(row =>
        row.map(escapeValue).join(',')
      )
    ].join('\n');
  }
}