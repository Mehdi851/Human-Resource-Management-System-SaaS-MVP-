import { Component, OnInit, inject, signal } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { SETTINGS_CONFIGURATION } from '../../data/settings.mock';

@Component({
  selector: 'app-security-settings',
  standalone: true,
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './security-settings.html',
  styleUrl: './security-settings.scss',
})
export class SecuritySettings implements OnInit {

  private readonly fb = inject(FormBuilder);

  readonly isSaving = signal(false);
  readonly saveSuccess = signal(false);

  readonly securityForm = this.fb.nonNullable.group({
    enforceStrongPassword: [true],

    enableLoginNotifications: [true],
  });

  ngOnInit(): void {
    this.loadSettings();
  }

  private loadSettings(): void {
    const settings = SETTINGS_CONFIGURATION.security;

    this.securityForm.patchValue({
      enforceStrongPassword: settings.enforceStrongPassword,
      enableLoginNotifications: settings.enableLoginNotifications,
    });

    this.securityForm.markAsPristine();
  }

  saveSettings(): void {
    this.saveSuccess.set(false);

    if (this.securityForm.invalid) {
      this.securityForm.markAllAsTouched();
      return;
    }

    this.isSaving.set(true);

    setTimeout(() => {

      console.log(
        'Security settings saved:',
        this.securityForm.getRawValue()
      );

      this.isSaving.set(false);
      this.saveSuccess.set(true);

      this.securityForm.markAsPristine();

    }, 700);
  }

  resetSettings(): void {
    this.loadSettings();
    this.saveSuccess.set(false);
  }
}