import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import {
  ActivatedRoute,
  Router,
  RouterLink
} from '@angular/router';

import { AttendanceModel } from '../models/attendance.model';
import { ATTENDANCE_RECORDS } from '../data/attendance.mock';

@Component({
  imports: [
    ReactiveFormsModule,
    RouterLink
  ],
  selector: 'app-attendance-form',
  styleUrl: './attendance-form.scss',
  templateUrl: './attendance-form.html',
})
export class AttendanceForm {
     private readonly fb = inject(FormBuilder);
  private readonly router = inject(Router);
  private readonly route = inject(ActivatedRoute);

  readonly employees = [
    {
      id: 'EMP-001',
      name: 'Ahmed Khan',
      department: 'Engineering'
    },
    {
      id: 'EMP-002',
      name: 'Sara Ahmed',
      department: 'Human Resources'
    },
    {
      id: 'EMP-003',
      name: 'Bilal Hussain',
      department: 'Finance'
    },
    {
      id: 'EMP-004',
      name: 'Ayesha Malik',
      department: 'Sales'
    },
    {
      id: 'EMP-005',
      name: 'Usman Ali',
      department: 'Operations'
    }
  ];

  readonly attendanceStatuses = [
    'Present',
    'Absent',
    'Late',
    'Half Day',
    'On Leave'
  ];

  readonly attendanceForm = this.fb.nonNullable.group({
    employeeId: ['', Validators.required],
    date: ['', Validators.required],
    checkIn: [''],
    checkOut: [''],
    status: ['Present', Validators.required],
    remarks: ['', Validators.maxLength(500)]
  });

  /*
   * Determine edit mode from the route path.
   *
   * /attendance/create       -> Create
   * /attendance/1/edit       -> Edit
   */
  readonly isEditMode =
    this.route.snapshot.url.some(
      segment => segment.path === 'edit'
    );

  readonly recordId = this.isEditMode
    ? Number(this.route.snapshot.paramMap.get('id'))
    : null;

  readonly existingRecord: AttendanceModel | undefined =
    this.isEditMode && this.recordId !== null
      ? ATTENDANCE_RECORDS.find(
          record => record.id === this.recordId
        )
      : undefined;

  get employeeId() {
    return this.attendanceForm.controls.employeeId;
  }

  get date() {
    return this.attendanceForm.controls.date;
  }

  get status() {
    return this.attendanceForm.controls.status;
  }

  get remarks() {
    return this.attendanceForm.controls.remarks;
  }

  constructor() {
    if (this.existingRecord) {
      this.attendanceForm.patchValue({
        employeeId: this.existingRecord.employeeId,
        date: this.existingRecord.date,
        checkIn: this.existingRecord.checkIn ?? '',
        checkOut: this.existingRecord.checkOut ?? '',
        status: this.existingRecord.status,
        remarks: this.existingRecord.remarks ?? ''
      });
    }
  }

  isInvalid(
    controlName:
      | 'employeeId'
      | 'date'
      | 'status'
      | 'remarks'
  ): boolean {
    const control =
      this.attendanceForm.controls[controlName];

    return (
      control.invalid &&
      (control.touched || control.dirty)
    );
  }

  saveAttendance(): void {
    if (this.attendanceForm.invalid) {
      this.attendanceForm.markAllAsTouched();
      return;
    }

    const formValue =
      this.attendanceForm.getRawValue();

    if (this.isEditMode) {
      console.log(
        'Update Attendance:',
        this.recordId,
        formValue
      );
    } else {
      console.log(
        'Create Attendance:',
        formValue
      );
    }

    // Phase 2: frontend only.
    // API/database persistence will be implemented in Phase 3.

    this.router.navigate(['/attendance']);
  }

  cancel(): void {
    if (this.isEditMode && this.recordId !== null) {
      this.router.navigate([
        '/attendance',
        this.recordId
      ]);

      return;
    }

    this.router.navigate(['/attendance']);
  }
}
