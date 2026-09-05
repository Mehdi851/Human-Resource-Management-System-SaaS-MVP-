import {
  ChangeDetectionStrategy,
  Component,
  computed,
  inject
} from '@angular/core';
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

import {
  DepartmentStatus
} from '../models/department.model';

import {
  DEPARTMENT_RECORDS
} from '../data/department.mock';

@Component({
  imports: [ReactiveFormsModule,
    RouterLink],
  selector: 'app-department-form',
  styleUrl: './department-form.scss',
  templateUrl: './department-form.html',
})
export class DepartmentForm {
  private readonly formBuilder = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  readonly departmentRecords = DEPARTMENT_RECORDS;

  readonly statuses: DepartmentStatus[] = [
    'Active',
    'Inactive'
  ];

  readonly departmentId = computed(() =>
    Number(this.route.snapshot.paramMap.get('id'))
  );

  readonly isEditMode = computed(() =>
    !!this.route.snapshot.paramMap.get('id')
  );

  readonly pageTitle = computed(() =>
    this.isEditMode()
      ? 'Edit Department'
      : 'Create Department'
  );

  readonly pageSubtitle = computed(() =>
    this.isEditMode()
      ? 'Update department information and configuration.'
      : 'Create a new department for your organization.'
  );

  readonly department = computed(() => {
    if (!this.isEditMode()) {
      return undefined;
    }

    return this.departmentRecords.find(
      department => department.id === this.departmentId()
    );
  });

  readonly departmentForm = this.formBuilder.group({
    name: [
      '',
      [
        Validators.required,
        Validators.maxLength(100)
      ]
    ],

    description: [
      '',
      [
        Validators.maxLength(500)
      ]
    ],

    manager: [
      '',
      [
        Validators.required,
        Validators.maxLength(100)
      ]
    ],

    status: [
      'Active' as DepartmentStatus,
      [
        Validators.required
      ]
    ]
  });

  submitted = false;

  constructor() {
    const department = this.department();

    if (department) {
      this.departmentForm.patchValue({
        name: department.name,
        description: department.description,
        manager: department.manager,
        status: department.status
      });
    }
  }

  get name() {
    return this.departmentForm.controls.name;
  }

  get description() {
    return this.departmentForm.controls.description;
  }

  get manager() {
    return this.departmentForm.controls.manager;
  }

  get status() {
    return this.departmentForm.controls.status;
  }

  submit(): void {
    this.submitted = true;

    if (this.departmentForm.invalid) {
      this.departmentForm.markAllAsTouched();
      return;
    }

    /*
     * Phase 2 UI only.
     *
     * No API call or persistence is performed here.
     * Backend integration will be added in Phase 3.
     */

    this.router.navigate(['/departments']);
  }

  cancel(): void {
    this.router.navigate(['/departments']);
  }
}
