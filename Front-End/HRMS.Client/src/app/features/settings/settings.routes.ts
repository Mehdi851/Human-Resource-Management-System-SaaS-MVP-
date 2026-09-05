import { Routes } from '@angular/router';

export const SETTINGS_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./components/settings-layout/settings-layout')
        .then((m) => m.SettingsLayout),

    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'general',
      },

      {
        path: 'general',
        loadComponent: () =>
          import('./components/general-settings/general-settings')
            .then((m) => m.GeneralSettings),
      },

      {
        path: 'work',
        loadComponent: () =>
          import('./components/work-settings/work-settings')
            .then((m) => m.WorkSettings),
      },

      {
        path: 'leave',
        loadComponent: () =>
          import('./components/leave-settings/leave-settings')
            .then((m) => m.LeaveSettings),
      },

      {
        path: 'payroll',
        loadComponent: () =>
          import('./components/payroll-settings/payroll-settings')
            .then((m) => m.PayrollSettings),
      },

      {
        path: 'notifications',
        loadComponent: () =>
          import('./components/notification-settings/notification-settings')
            .then((m) => m.NotificationSettings),
      },

      {
        path: 'security',
        loadComponent: () =>
          import('./components/security-settings/security-settings')
            .then((m) => m.SecuritySettings),
      },

      {
        path: 'system',
        loadComponent: () =>
          import('./components/system-settings/system-settings')
            .then((m) => m.SystemSettings),
      },
    ],
  },
];