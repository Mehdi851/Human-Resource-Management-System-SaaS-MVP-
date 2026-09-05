import { Component, OnInit, inject, signal } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';

import { SETTINGS_CONFIGURATION } from '../../data/settings.mock';

@Component({
  selector: 'app-work-settings',
  standalone: true,
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './work-settings.html',
  styleUrl: './work-settings.scss',
})
export class WorkSettings implements OnInit {

  private readonly fb = inject(FormBuilder);

  readonly isSaving = signal(false);
  readonly saveSuccess = signal(false);

  readonly workForm = this.fb.nonNullable.group({
    monday: [false],
    tuesday: [false],
    wednesday: [false],
    thursday: [false],
    friday: [false],
    saturday: [false],
    sunday: [false],

    workStartTime: [
      '',
      Validators.required,
    ],

    workEndTime: [
      '',
      Validators.required,
    ],

    gracePeriodMinutes: [
      15,
      [
        Validators.required,
        Validators.min(0),
        Validators.max(120),
      ],
    ],

    enableLateTracking: [true],
  });

  ngOnInit(): void {
    this.loadSettings();
  }

  private loadSettings(): void {
    const settings = SETTINGS_CONFIGURATION.work;

    this.workForm.patchValue({
      monday: settings.workingDays.includes('Monday'),
      tuesday: settings.workingDays.includes('Tuesday'),
      wednesday: settings.workingDays.includes('Wednesday'),
      thursday: settings.workingDays.includes('Thursday'),
      friday: settings.workingDays.includes('Friday'),
      saturday: settings.workingDays.includes('Saturday'),
      sunday: settings.workingDays.includes('Sunday'),

      workStartTime: settings.workStartTime,
      workEndTime: settings.workEndTime,
      gracePeriodMinutes: settings.gracePeriodMinutes,
      enableLateTracking: settings.enableLateTracking,
    });

    this.workForm.markAsPristine();
  }

  saveSettings(): void {
    this.saveSuccess.set(false);

    if (this.workForm.invalid) {
      this.workForm.markAllAsTouched();
      return;
    }

    if (!this.hasWorkingDaySelected()) {
      return;
    }

    this.isSaving.set(true);

    setTimeout(() => {

      const formValue = this.workForm.getRawValue();

      const workingDays: string[] = [];

      if (formValue.monday) workingDays.push('Monday');
      if (formValue.tuesday) workingDays.push('Tuesday');
      if (formValue.wednesday) workingDays.push('Wednesday');
      if (formValue.thursday) workingDays.push('Thursday');
      if (formValue.friday) workingDays.push('Friday');
      if (formValue.saturday) workingDays.push('Saturday');
      if (formValue.sunday) workingDays.push('Sunday');

      console.log('Work settings saved:', {
        workingDays,
        workStartTime: formValue.workStartTime,
        workEndTime: formValue.workEndTime,
        gracePeriodMinutes: formValue.gracePeriodMinutes,
        enableLateTracking: formValue.enableLateTracking,
      });

      this.isSaving.set(false);
      this.saveSuccess.set(true);

      this.workForm.markAsPristine();

    }, 700);
  }

  resetSettings(): void {
    this.loadSettings();
    this.saveSuccess.set(false);
  }

  hasWorkingDaySelected(): boolean {
    const value = this.workForm.getRawValue();

    return (
      value.monday ||
      value.tuesday ||
      value.wednesday ||
      value.thursday ||
      value.friday ||
      value.saturday ||
      value.sunday
    );
  }

  get workStartTime() {
    return this.workForm.controls.workStartTime;
  }

  get workEndTime() {
    return this.workForm.controls.workEndTime;
  }

  get gracePeriodMinutes() {
    return this.workForm.controls.gracePeriodMinutes;
  }
}