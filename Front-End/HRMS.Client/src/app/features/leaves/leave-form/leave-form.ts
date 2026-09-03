import {Component,computed,inject} from '@angular/core';
import {FormBuilder,ReactiveFormsModule,Validators} from '@angular/forms';
import {ActivatedRoute,Router,RouterLink} from '@angular/router';
import {LeaveType} from '../models/leave.model';
import {LEAVE_REQUESTS} from '../data/leave.mock';

@Component({
  imports: [ReactiveFormsModule,RouterLink],
  selector: 'app-leave-form',
  styleUrl: './leave-form.scss',
  templateUrl: './leave-form.html',
})
export class LeaveForm {
  private readonly fb = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  readonly leaveTypes: LeaveType[] = [
    'Annual',
    'Sick',
    'Casual',
    'Unpaid',
    'Maternity',
    'Paternity'
  ];

  readonly leaveId = computed(() =>
    Number(this.route.snapshot.paramMap.get('id'))
  );

  readonly isEditMode = computed(() =>
    !!this.route.snapshot.paramMap.get('id')
  );

  readonly leaveRequest = computed(() =>
    LEAVE_REQUESTS.find(
      leave => leave.id === this.leaveId()
    )
  );

  readonly form = this.fb.nonNullable.group({
    employee: [
      '',
      [Validators.required]
    ],

    leaveType: [
      '' as LeaveType | '',
      [Validators.required]
    ],

    startDate: [
      '',
      [Validators.required]
    ],

    endDate: [
      '',
      [Validators.required]
    ],

    reason: [
      '',
      [
        Validators.required,
        Validators.minLength(10),
        Validators.maxLength(500)
      ]
    ]
  });

  constructor() {
    this.populateEditForm();
  }

  private populateEditForm(): void {

    if (!this.isEditMode()) {
      return;
    }

    const leave = this.leaveRequest();

    if (!leave) {
      return;
    }

    this.form.patchValue({
      employee: leave.employeeCode,
      leaveType: leave.leaveType,
      startDate: leave.startDate,
      endDate: leave.endDate,
      reason: leave.reason
    });
  }

  get employeeControl() {
    return this.form.controls.employee;
  }

  get leaveTypeControl() {
    return this.form.controls.leaveType;
  }

  get startDateControl() {
    return this.form.controls.startDate;
  }

  get endDateControl() {
    return this.form.controls.endDate;
  }

  get reasonControl() {
    return this.form.controls.reason;
  }

  isFieldInvalid(
    control: typeof this.form.controls.employee
  ): boolean {
    return control.invalid && control.touched;
  }

  getDateError(): string | null {

    if (
      !this.startDateControl.value ||
      !this.endDateControl.value
    ) {
      return null;
    }

    if (
      this.endDateControl.value <
      this.startDateControl.value
    ) {
      return 'End date cannot be earlier than the start date.';
    }

    return null;
  }

  submit(): void {

    this.form.markAllAsTouched();

    if (
      this.form.invalid ||
      this.getDateError()
    ) {
      return;
    }

    console.log(
      this.isEditMode()
        ? 'Update leave request'
        : 'Create leave request',
      this.form.getRawValue()
    );

    this.router.navigate(['/leaves']);
  }

  cancel(): void {
    this.router.navigate(['/leaves']);
  }
}
