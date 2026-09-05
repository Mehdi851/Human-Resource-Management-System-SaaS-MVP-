import { Routes } from '@angular/router';

export const ORGANIZATION_ROUTES: Routes = [
  {
    path: '',
    loadComponent: () =>
      import('./organization')
        .then(m => m.Organizations)
  },

  {
    path: 'create',
    loadComponent: () =>
      import('./organization-form/organization-form')
        .then(m => m.OrganizationForm)
  },

  {
    path: ':id/edit',
    loadComponent: () =>
      import('./organization-form/organization-form')
        .then(m => m.OrganizationForm)
  },

  {
    path: ':id',
    loadComponent: () =>
      import('./organization-details/organization-details')
        .then(m => m.OrganizationDetails)
  }
];