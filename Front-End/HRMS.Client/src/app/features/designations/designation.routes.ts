import { Routes } from '@angular/router';

export const DESIGNATION_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./designations')
        .then(m => m.Designations),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./designation-form/designation-form')
        .then(m => m.DesignationForm),
  },
  {
    path: ':id/edit',
    loadComponent: () =>
      import('./designation-form/designation-form')
        .then(m => m.DesignationForm),
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./designation-details/designation-details')
        .then(m => m.DesignationDetails),
  },
];