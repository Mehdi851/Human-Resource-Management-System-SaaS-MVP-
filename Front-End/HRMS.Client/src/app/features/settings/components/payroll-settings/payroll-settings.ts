import { Component, OnInit, inject, signal } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { SETTINGS_CONFIGURATION } from '../../data/settings.mock';

@Component({
  selector: 'app-payroll-settings',
  standalone: true,
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './payroll-settings.html',
  styleUrl: './payroll-settings.scss',
})
export class PayrollSettings implements OnInit {

  private readonly fb = inject(FormBuilder);

  readonly isSaving = signal(false);
  readonly saveSuccess = signal(false);

  readonly payrollForm = this.fb.nonNullable.group({
    payFrequency: [
      '',
      Validators.required,
    ],

    currency: [
      '',
      Validators.required,
    ],

    payDay: [
      25,
      [
        Validators.required,
        Validators.min(1),
        Validators.max(31),
      ],
    ],
  });

  ngOnInit(): void {
    this.loadSettings();
  }

  private loadSettings(): void {
    const settings = SETTINGS_CONFIGURATION.payroll;

    this.payrollForm.patchValue({
      payFrequency: settings.payFrequency,
      currency: settings.currency,
      payDay: settings.payDay,
    });

    this.payrollForm.markAsPristine();
  }

  saveSettings(): void {
    this.saveSuccess.set(false);

    if (this.payrollForm.invalid) {
      this.payrollForm.markAllAsTouched();
      return;
    }

    this.isSaving.set(true);

    setTimeout(() => {

      console.log(
        'Payroll settings saved:',
        this.payrollForm.getRawValue()
      );

      this.isSaving.set(false);
      this.saveSuccess.set(true);

      this.payrollForm.markAsPristine();

    }, 700);
  }

  resetSettings(): void {
    this.loadSettings();
    this.saveSuccess.set(false);
  }

  get payFrequency() {
    return this.payrollForm.controls.payFrequency;
  }

  get currency() {
    return this.payrollForm.controls.currency;
  }

  get payDay() {
    return this.payrollForm.controls.payDay;
  }
}