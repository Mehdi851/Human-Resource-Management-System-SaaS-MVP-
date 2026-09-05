import {
  ChangeDetectionStrategy,
  Component,
  computed,
  signal
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

import { DESIGNATION_MOCK_DATA } from '../data/designation.mock';
import {
  DesignationModel,
  DesignationStatus
} from '../models/designation.model';

@Component({
  imports: [RouterLink,ReactiveFormsModule],
  selector: 'app-designation-form',
  styleUrl: './designation-form.scss',
  templateUrl: './designation-form.html',
})
export class DesignationForm {
   readonly isEditMode = signal(false);

  readonly designation = signal<DesignationModel | null>(null);

  readonly submitted = signal(false);

  readonly isNotFound = signal(false);

  readonly pageTitle = computed(() =>
    this.isEditMode()
      ? 'Edit Designation'
      : 'Create Designation'
  );

  readonly submitButtonText = computed(() =>
    this.isEditMode()
      ? 'Save Changes'
      : 'Create Designation'
  );

  readonly designationForm;

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly route: ActivatedRoute,
    private readonly router: Router
  ) {
    this.designationForm = this.formBuilder.nonNullable.group({
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

      department: [
        '',
        [
          Validators.required,
          Validators.maxLength(100)
        ]
      ],

      status: [
        'Active' as DesignationStatus,
        [
          Validators.required
        ]
      ]
    });

    this.initializeForm();
  }

  private initializeForm(): void {
    const editId = this.route.snapshot.paramMap.get('id');

    // Create mode
    if (!editId) {
      return;
    }

    const id = Number(editId);

    // Invalid route ID
    if (!Number.isInteger(id)) {
      this.isNotFound.set(true);
      return;
    }

    this.isEditMode.set(true);

    const foundDesignation = DESIGNATION_MOCK_DATA.find(
      designation => designation.id === id
    );

    // Designation not found
    if (!foundDesignation) {
      this.isNotFound.set(true);
      return;
    }

    this.designation.set(foundDesignation);

    this.designationForm.patchValue({
      name: foundDesignation.name,
      description: foundDesignation.description,
      department: foundDesignation.department,
      status: foundDesignation.status
    });
  }

  isFieldInvalid(fieldName: string): boolean {
    const field = this.designationForm.get(fieldName);

    return !!(
      field &&
      field.invalid &&
      (field.touched || this.submitted())
    );
  }

  getFieldError(fieldName: string): string {
    const field = this.designationForm.get(fieldName);

    if (!field || !field.errors) {
      return '';
    }

    if (field.errors['required']) {
      return 'This field is required.';
    }

    if (field.errors['maxlength']) {
      return `Maximum ${field.errors['maxlength'].requiredLength} characters allowed.`;
    }

    return 'Please enter a valid value.';
  }

  onSubmit(): void {
    this.submitted.set(true);

    if (this.designationForm.invalid) {
      this.designationForm.markAllAsTouched();
      return;
    }

    // Phase 2: UI only.
    // No API call or persistence.
    this.router.navigate(['/designations']);
  }

  onCancel(): void {
    this.router.navigate(['/designations']);
  }
}
