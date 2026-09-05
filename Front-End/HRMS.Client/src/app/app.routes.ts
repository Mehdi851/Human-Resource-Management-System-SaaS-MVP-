import { Routes } from '@angular/router';

import { AppShellComponent } from './layout/app-shell/app-shell';
import { AuthLayout } from './features/auth/auth-layout/auth-layout';

export const routes: Routes = [
  {
    path: '',
    component: AppShellComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'dashboard',
      },

      {
        path: 'dashboard',
        loadChildren: () =>
          import('./features/dashboard/dashboard.routes')
            .then(m => m.DASHBOARD_ROUTES),
      },

      {
        path: 'employees',
        loadChildren: () =>
          import('./features/employees/employee.routes')
            .then(m => m.EMPLOYEE_ROUTES),
      },

      {
        path: 'departments',
        loadChildren: () =>
          import('./features/departments/department.routes')
            .then(m => m.DEPARTMENT_ROUTES),
      },

      {
        path: 'designations',
        loadChildren: () =>
          import('./features/designations/designation.routes')
            .then(m => m.DESIGNATION_ROUTES),
      },

      {
        path: 'attendance',
        loadChildren: () =>
          import('./features/attendance/attendance.routes')
            .then(m => m.ATTENDANCE_ROUTES),
      },

      {
        path: 'leaves',
        loadChildren: () =>
          import('./features/leaves/leave.routes')
            .then(m => m.LEAVE_ROUTES),
      },

      {
        path: 'payroll',
        loadChildren: () =>
          import('./features/payroll/payroll.routes')
            .then(m => m.PAYROLL_ROUTES),
      },

      {
        path: 'organization',
        loadChildren: () =>
          import('./features/organization/organization.routes')
            .then(m => m.ORGANIZATION_ROUTES),
      },

      {
        path: 'reports',
        loadChildren: () =>
          import('./features/reports/reports.routes')
            .then(m => m.REPORTS_ROUTES),
      },

      {
        path: 'settings',
        loadChildren: () =>
          import('./features/settings/settings.routes')
            .then(m => m.SETTINGS_ROUTES),
      },
    ],
  },

  {
    path: '',
    component: AuthLayout,
    children: [
      {
        path: '',
        loadChildren: () =>
          import('./features/auth/auth.routes')
            .then(m => m.AUTH_ROUTES),
      },
    ],
  },
];