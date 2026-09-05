import { Component, OnInit, inject, signal } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { SETTINGS_CONFIGURATION } from '../../data/settings.mock';

@Component({
  selector: 'app-leave-settings',
  standalone: true,
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './leave-settings.html',
  styleUrl: './leave-settings.scss',
})
export class LeaveSettings implements OnInit {

  private readonly fb = inject(FormBuilder);

  readonly isSaving = signal(false);
  readonly saveSuccess = signal(false);

  readonly leaveForm = this.fb.nonNullable.group({
    leaveYear: [
      2026,
      [
        Validators.required,
        Validators.min(2000),
        Validators.max(2100),
      ],
    ],

    requireApproval: [true],

    allowCarryForward: [true],

    maximumCarryForwardDays: [
      5,
      [
        Validators.required,
        Validators.min(0),
        Validators.max(365),
      ],
    ],
  });

  ngOnInit(): void {
    this.loadSettings();
  }

  private loadSettings(): void {
    const settings = SETTINGS_CONFIGURATION.leave;

    this.leaveForm.patchValue({
      leaveYear: settings.leaveYear,
      requireApproval: settings.requireApproval,
      allowCarryForward: settings.allowCarryForward,
      maximumCarryForwardDays: settings.maximumCarryForwardDays,
    });

    this.leaveForm.markAsPristine();
  }

  saveSettings(): void {
    this.saveSuccess.set(false);

    if (this.leaveForm.invalid) {
      this.leaveForm.markAllAsTouched();
      return;
    }

    const formValue = this.leaveForm.getRawValue();

    if (
      !formValue.allowCarryForward &&
      formValue.maximumCarryForwardDays > 0
    ) {
      this.leaveForm.controls.maximumCarryForwardDays.setValue(0);
    }

    this.isSaving.set(true);

    setTimeout(() => {

      console.log(
        'Leave settings saved:',
        this.leaveForm.getRawValue()
      );

      this.isSaving.set(false);
      this.saveSuccess.set(true);

      this.leaveForm.markAsPristine();

    }, 700);
  }

  resetSettings(): void {
    this.loadSettings();
    this.saveSuccess.set(false);
  }

  get leaveYear() {
    return this.leaveForm.controls.leaveYear;
  }

  get maximumCarryForwardDays() {
    return this.leaveForm.controls.maximumCarryForwardDays;
  }
}