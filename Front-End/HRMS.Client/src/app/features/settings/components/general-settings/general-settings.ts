import { Component, OnInit, inject, signal } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { SETTINGS_CONFIGURATION } from '../../data/settings.mock';

@Component({
  selector: 'app-general-settings',
  standalone: true,
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './general-settings.html',
  styleUrl: './general-settings.scss',
})
export class GeneralSettings implements OnInit {

  private readonly fb = inject(FormBuilder);

  readonly isSaving = signal(false);
  readonly saveSuccess = signal(false);

  readonly generalForm = this.fb.nonNullable.group({
    organizationName: [
      '',
      [
        Validators.required,
        Validators.maxLength(100),
      ],
    ],

    contactEmail: [
      '',
      [
        Validators.required,
        Validators.email,
        Validators.maxLength(150),
      ],
    ],

    phone: [
      '',
      [
        Validators.maxLength(30),
      ],
    ],

    address: [
      '',
      [
        Validators.maxLength(250),
      ],
    ],

    timezone: [
      '',
      Validators.required,
    ],

    dateFormat: [
      '',
      Validators.required,
    ],

    currency: [
      '',
      Validators.required,
    ],
  });

  ngOnInit(): void {
    this.loadSettings();
  }

  private loadSettings(): void {
    const settings = SETTINGS_CONFIGURATION.general;

    this.generalForm.patchValue({
      organizationName: settings.organizationName,
      contactEmail: settings.contactEmail,
      phone: settings.phone,
      address: settings.address,
      timezone: settings.timezone,
      dateFormat: settings.dateFormat,
      currency: settings.currency,
    });

    this.generalForm.markAsPristine();
  }

  saveSettings(): void {
    this.saveSuccess.set(false);

    if (this.generalForm.invalid) {
      this.generalForm.markAllAsTouched();
      return;
    }

    this.isSaving.set(true);

    setTimeout(() => {
      console.log(
        'General settings saved:',
        this.generalForm.getRawValue()
      );

      this.isSaving.set(false);
      this.saveSuccess.set(true);

      this.generalForm.markAsPristine();
    }, 700);
  }

  resetSettings(): void {
    this.loadSettings();
    this.saveSuccess.set(false);
  }

  get organizationName() {
    return this.generalForm.controls.organizationName;
  }

  get contactEmail() {
    return this.generalForm.controls.contactEmail;
  }

  get phone() {
    return this.generalForm.controls.phone;
  }

  get address() {
    return this.generalForm.controls.address;
  }

  get timezone() {
    return this.generalForm.controls.timezone;
  }

  get dateFormat() {
    return this.generalForm.controls.dateFormat;
  }

  get currency() {
    return this.generalForm.controls.currency;
  }
}