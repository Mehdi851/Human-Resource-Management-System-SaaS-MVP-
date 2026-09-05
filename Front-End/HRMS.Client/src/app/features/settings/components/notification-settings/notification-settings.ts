import { Component, OnInit, inject, signal } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
} from '@angular/forms';

import { SETTINGS_CONFIGURATION } from '../../data/settings.mock';

@Component({
  selector: 'app-notification-settings',
  standalone: true,
  imports: [
    ReactiveFormsModule,
  ],
  templateUrl: './notification-settings.html',
  styleUrl: './notification-settings.scss',
})
export class NotificationSettings implements OnInit {

  private readonly fb = inject(FormBuilder);

  readonly isSaving = signal(false);
  readonly saveSuccess = signal(false);

  readonly notificationForm = this.fb.nonNullable.group({
    emailNotifications: [true],
    leaveNotifications: [true],
    attendanceNotifications: [true],
    payrollNotifications: [true],
  });

  ngOnInit(): void {
    this.loadSettings();
  }

  private loadSettings(): void {
    const settings = SETTINGS_CONFIGURATION.notifications;

    this.notificationForm.patchValue({
      emailNotifications: settings.emailNotifications,
      leaveNotifications: settings.leaveNotifications,
      attendanceNotifications: settings.attendanceNotifications,
      payrollNotifications: settings.payrollNotifications,
    });

    this.notificationForm.markAsPristine();
  }

  saveSettings(): void {
    this.saveSuccess.set(false);
    this.isSaving.set(true);

    setTimeout(() => {

      console.log(
        'Notification settings saved:',
        this.notificationForm.getRawValue()
      );

      this.isSaving.set(false);
      this.saveSuccess.set(true);

      this.notificationForm.markAsPristine();

    }, 700);
  }

  resetSettings(): void {
    this.loadSettings();
    this.saveSuccess.set(false);
  }
}