import { Routes } from '@angular/router';
import { AppShellComponent } from './layout/app-shell/app-shell';
import { AuthLayout } from './features/auth/auth-layout/auth-layout';
import { Login } from './features/auth/login/login';
import { ForgotPassword } from './features/auth/forgot-password/forgot-password';
import { ResetPassword } from './features/auth/reset-password/reset-password';
import { EmployeeDetails } from './features/employees/employee-details/employee-details';
import { Employees } from './features/employees/employees';
import { EmployeeForm } from './features/employees/employee-form/employee-form';
import { LeaveForm } from './features/leaves/leave-form/leave-form';
import { LeaveDetails } from './features/leaves/leave-details/leave-details';
import { Leave } from './features/leaves/leave';

export const routes: Routes = [
    {
    path: '',
    component: AppShellComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        redirectTo: 'dashboard'
      },
      {
        path: 'dashboard',
        loadComponent: () =>
          import('./features/dashboard/dashboard')
            .then(m => m.Dashboard)
      },
      {
        path: 'employees',
        children: [
          {
            path: '',
            component: Employees,
          },
          {
          path: 'create',
          component: EmployeeForm,
          },
          {
            path: ':id/edit',
            component: EmployeeForm,
          },
          {
            path: ':id',
            component: EmployeeDetails,
          },
        ],
      },
      {
        path: 'departments',
        loadComponent: () =>
          import('./features/departments/departments')
            .then(m => m.Departments)
      },
      {
        path: 'designations',
        loadComponent: () =>
          import('./features/designations/designations')
            .then(m => m.Designations)
      },
      {
        path: 'attendance',
        loadComponent: () =>
          import('./features/attendance/attendance')
            .then(m => m.Attendance)
      },
      {
        path: 'leaves',
        children: [
          {
            path: '',
            component: Leave,
          },
          {
            path: 'create',
            component: LeaveForm,
          },
          {
            path: ':id/edit',
            component: LeaveForm,
          },
          {
            path: ':id',
            component: LeaveDetails,
          },
        ],
      },
      {
        path: 'payroll',
        loadComponent: () =>
          import('./features/payroll/payroll')
            .then(m => m.Payroll)
      },
      {
        path: 'organization',
        loadComponent: () =>
          import('./features/organization/organization')
            .then(m => m.Organization)
      }
    ],
    },
    {
    path: '',
    component: AuthLayout,
    children: [
      {
        path: 'login',
        component: Login,
      },
      {
      path: 'forgot-password',
      component: ForgotPassword,
      },
      {
      path: 'reset-password',
      component: ResetPassword,
      },
    ],
},
];
