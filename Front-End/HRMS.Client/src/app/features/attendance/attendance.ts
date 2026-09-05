import { ChangeDetectionStrategy, Component, computed, signal } from '@angular/core';
import { AttendanceStatus,AttendanceModel } from './models/attendance.model';
import { ATTENDANCE_RECORDS } from './data/attendance.mock';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
@Component({
  imports: [FormsModule,
    RouterLink],
  standalone: true,
  selector: 'app-attendance',
  styleUrl: './attendance.scss',
  templateUrl: './attendance.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Attendance {
    readonly attendanceRecords = ATTENDANCE_RECORDS;

  readonly searchTerm = signal('');
  readonly selectedDepartment = signal('');
  readonly selectedStatus = signal<AttendanceStatus | ''>('');
  readonly selectedDate = signal('');

  readonly departments = [
    'Engineering',
    'HR',
    'Finance',
    'Sales',
    'Operations'
  ];

  readonly statuses: AttendanceStatus[] = [
    'Present',
    'Absent',
    'Late',
    'Half Day',
    'On Leave'
  ];

  readonly filteredRecords = computed<AttendanceModel[]>(() => {
    const search = this.searchTerm().trim().toLowerCase();
    const department = this.selectedDepartment();
    const status = this.selectedStatus();
    const date = this.selectedDate();

    return this.attendanceRecords.filter(record => {
      const matchesSearch =
        !search ||
        record.employeeName.toLowerCase().includes(search) ||
        record.employeeId.toLowerCase().includes(search);

      const matchesDepartment =
        !department ||
        record.department === department;

      const matchesStatus =
        !status ||
        record.status === status;

      const matchesDate =
        !date ||
        record.date === date;

      return (
        matchesSearch &&
        matchesDepartment &&
        matchesStatus &&
        matchesDate
      );
    });
  });

  updateSearch(value: string): void {
    this.searchTerm.set(value);
  }

  updateDepartment(value: string): void {
    this.selectedDepartment.set(value);
  }

  updateStatus(value: string): void {
    this.selectedStatus.set(value as AttendanceStatus | '');
  }

  updateDate(value: string): void {
    this.selectedDate.set(value);
  }

  clearFilters(): void {
    this.searchTerm.set('');
    this.selectedDepartment.set('');
    this.selectedStatus.set('');
    this.selectedDate.set('');
  }
}
