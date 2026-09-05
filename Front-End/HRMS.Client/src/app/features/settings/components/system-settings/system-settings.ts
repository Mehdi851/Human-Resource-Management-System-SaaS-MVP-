import { Component, OnInit, inject, signal } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { SETTINGS_CONFIGURATION } from '../../data/settings.mock';

@Component({
  selector: 'app-system-settings',
  standalone: true,
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './system-settings.html',
  styleUrl: './system-settings.scss',
})
export class SystemSettings implements OnInit {

  private readonly fb = inject(FormBuilder);

  readonly isSaving = signal(false);
  readonly saveSuccess = signal(false);

  readonly systemForm = this.fb.nonNullable.group({
    language: [
      'English',
      Validators.required,
    ],

    theme: [
      'light' as 'light' | 'dark' | 'system',
      Validators.required,
    ],

    defaultDashboard: [
      'HR Dashboard',
      Validators.required,
    ],
  });

  ngOnInit(): void {
    this.loadSettings();
  }

  private loadSettings(): void {
    const settings = SETTINGS_CONFIGURATION.system;

    this.systemForm.patchValue({
      language: settings.language,
      theme: settings.theme,
      defaultDashboard: settings.defaultDashboard,
    });

    this.systemForm.markAsPristine();
  }

  saveSettings(): void {
    this.saveSuccess.set(false);

    if (this.systemForm.invalid) {
      this.systemForm.markAllAsTouched();
      return;
    }

    this.isSaving.set(true);

    setTimeout(() => {

      console.log(
        'System settings saved:',
        this.systemForm.getRawValue()
      );

      this.isSaving.set(false);
      this.saveSuccess.set(true);

      this.systemForm.markAsPristine();

    }, 700);
  }

  resetSettings(): void {
    this.loadSettings();
    this.saveSuccess.set(false);
  }
}