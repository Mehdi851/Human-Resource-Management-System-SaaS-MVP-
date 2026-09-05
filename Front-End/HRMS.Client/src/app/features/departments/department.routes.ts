import { Routes } from '@angular/router';

export const DEPARTMENT_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./departments')
        .then(m => m.Departments),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./department-form/department-form')
        .then(m => m.DepartmentForm),
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./department-details/department-details')
        .then(m => m.DepartmentDetails),
  },
];