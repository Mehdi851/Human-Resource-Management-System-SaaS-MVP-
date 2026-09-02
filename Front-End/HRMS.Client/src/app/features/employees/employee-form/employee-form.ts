import {
  ChangeDetectionStrategy,Component,computed,inject,} from '@angular/core';
import { CommonModule } from '@angular/common';
import {FormBuilder,ReactiveFormsModule, Validators,} from '@angular/forms';
import { ActivatedRoute,Router,RouterLink,} from '@angular/router';

import { MOCK_EMPLOYEES } from '../data/employee.mock';
import { Employee } from '../models/employee.model';

@Component({
  imports: [CommonModule,
    ReactiveFormsModule,
    RouterLink,],
  selector: 'app-employee-form',
  styleUrl: './employee-form.scss',
  templateUrl: './employee-form.html',
})
export class EmployeeForm {
   private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  readonly employeeId =
    this.route.snapshot.paramMap.get('id');

  readonly isEditMode =
    !!this.employeeId;

  readonly employee = computed<
    Employee | undefined
  >(() => {
    if (!this.employeeId) {
      return undefined;
    }

    return MOCK_EMPLOYEES.find(
      employee =>
        employee.id === this.employeeId,
    );
  });

  readonly pageTitle = this.isEditMode
    ? 'Edit Employee'
    : 'Add Employee';

  readonly pageDescription =
    this.isEditMode
      ? 'Update employee information and employment details.'
      : 'Add a new employee to your organization.';

  readonly employeeForm = this.fb.nonNullable.group({
    firstName: [
      '',
      [
        Validators.required,
        Validators.maxLength(50),
      ],
    ],

    lastName: [
      '',
      [
        Validators.required,
        Validators.maxLength(50),
      ],
    ],

    email: [
      '',
      [
        Validators.required,
        Validators.email,
      ],
    ],

    phone: [
      '',
      [
        Validators.required,
        Validators.maxLength(30),
      ],
    ],

    employeeCode: [
      '',
      [
        Validators.required,
        Validators.maxLength(30),
      ],
    ],

    department: [
      '',
      Validators.required,
    ],

    designation: [
      '',
      Validators.required,
    ],

    joiningDate: [
      '',
      Validators.required,
    ],

    employmentStatus: [
      'Active',
      Validators.required,
    ],
  });

  readonly departments = [
    'Engineering',
    'Human Resources',
    'Finance',
    'Sales',
    'Marketing',
    'Operations',
  ];

  readonly designations = [
    'Software Engineer',
    'Senior Software Engineer',
    'HR Manager',
    'Accountant',
    'Sales Executive',
    'Marketing Specialist',
    'Operations Manager',
  ];

  readonly statuses = [
    'Active',
    'Inactive',
    'On Leave',
  ];

  constructor() {
    this.populateEditForm();
  }

  private populateEditForm(): void {
    const employee = this.employee();

    if (!employee) {
      return;
    }

    this.employeeForm.patchValue({
      firstName: employee.firstName,
      lastName: employee.lastName,
      email: employee.email,
      phone: employee.phone,
      employeeCode: employee.employeeCode,
      department: employee.department,
      designation: employee.designation,
      joiningDate: employee.joiningDate,
      employmentStatus:
        employee.employmentStatus,
    });
  }

  isInvalid(
    controlName: string,
  ): boolean {
    const control =
      this.employeeForm.get(controlName);

    return !!(
      control &&
      control.invalid &&
      (control.dirty || control.touched)
    );
  }

  save(): void {
    if (this.employeeForm.invalid) {
      this.employeeForm.markAllAsTouched();
      return;
    }

    /*
     * Phase 2:
     * Frontend-only implementation.
     *
     * API persistence will be implemented
     * during Phase 3.
     */

    console.log(
      'Employee form submitted:',
      this.employeeForm.getRawValue(),
    );
  }

  cancel(): void {
    this.router.navigate([
      '/employees',
    ]);
  }
}
