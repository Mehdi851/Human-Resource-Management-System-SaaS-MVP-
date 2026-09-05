import { Routes } from '@angular/router';

export const EMPLOYEE_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./employees')
        .then(m => m.Employees),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./employee-form/employee-form')
        .then(m => m.EmployeeForm),
  },
  {
    path: ':id/edit',
    loadComponent: () =>
      import('./employee-form/employee-form')
        .then(m => m.EmployeeForm),
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./employee-details/employee-details')
        .then(m => m.EmployeeDetails),
  },
];