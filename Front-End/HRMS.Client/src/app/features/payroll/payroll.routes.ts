import { Routes } from '@angular/router';

export const PAYROLL_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./payroll')
        .then(m => m.Payroll),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./payroll-form/payroll-form')
        .then(m => m.PayrollForm),
  },
  {
    path: ':id/edit',
    loadComponent: () =>
      import('./payroll-form/payroll-form')
        .then(m => m.PayrollForm),
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./payroll-details/payroll-details')
        .then(m => m.PayrollDetails),
  },
];