import { Routes } from '@angular/router';

export const LEAVE_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./leave')
        .then(m => m.Leave),
  },
  {
    path: 'create',
    loadComponent: () =>
      import('./leave-form/leave-form')
        .then(m => m.LeaveForm),
  },
  {
    path: ':id/edit',
    loadComponent: () =>
      import('./leave-form/leave-form')
        .then(m => m.LeaveForm),
  },
  {
    path: ':id',
    loadComponent: () =>
      import('./leave-details/leave-details')
        .then(m => m.LeaveDetails),
  },
];