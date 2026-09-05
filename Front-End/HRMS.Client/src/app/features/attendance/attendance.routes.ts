import { Routes } from '@angular/router';

export const ATTENDANCE_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./attendance')
        .then(m => m.Attendance),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./attendance-form/attendance-form')
        .then(m => m.AttendanceForm),
  },
  {
    path: ':id/edit',
    loadComponent: () =>
      import('./attendance-form/attendance-form')
        .then(m => m.AttendanceForm),
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./attendance-details/attendance-details')
        .then(m => m.AttendanceDetails),
  },
];