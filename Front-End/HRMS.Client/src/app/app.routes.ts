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
    children: [
      {
        path: '',
        loadComponent: () =>
          import('./features/departments/departments')
            .then(m => m.Departments)
      },
      {
      path: 'create',
      loadComponent: () =>
        import('./features/departments/department-form/department-form')
          .then(m => m.DepartmentForm)
    },
      
      {
        path: ':id',
        loadComponent: () =>
          import('./features/departments/department-details/department-details')
            .then(m => m.DepartmentDetails)
      }
    ]
  },
      {
        path: 'designations',
        children: [
          {
            path: '',
            loadComponent: () =>
              import('./features/designations/designations')
                .then(m => m.Designations)
          },
          {
            path: 'create',
            loadComponent: () =>
              import('./features/designations/designation-form/designation-form')
                .then(m => m.DesignationForm)
          },
          {
            path: ':id/edit',
            loadComponent: () =>
              import('./features/designations/designation-form/designation-form')
                .then(m => m.DesignationForm)
          },
          {
            path: ':id',
            loadComponent: () =>
              import('./features/designations/designation-details/designation-details')
                .then(m => m.DesignationDetails)
          }
        ]
      },
      {
        path: 'attendance',
        loadComponent: () =>
          import('./features/attendance/attendance')
            .then(m => m.Attendance)
      },
      {
        path: 'attendance/create',
        loadComponent: () =>
          import(
            './features/attendance/attendance-form/attendance-form'
          ).then(m => m.AttendanceForm)
      },

      {
        path: 'attendance/:id/edit',
        loadComponent: () =>
          import(
            './features/attendance/attendance-form/attendance-form'
          ).then(m => m.AttendanceForm)
      },

      {
        path: 'attendance/:id',
        loadComponent: () =>
          import(
            './features/attendance/attendance-details/attendance-details'
          ).then(m => m.AttendanceDetails)
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
            .then(component => component.Payroll)
      },

      {
        path: 'payroll/create',
        loadComponent: () =>
          import('./features/payroll/payroll-form/payroll-form')
            .then(component => component.PayrollForm)
      },

      {
        path: 'payroll/:id/edit',
        loadComponent: () =>
          import('./features/payroll/payroll-form/payroll-form')
            .then(component => component.PayrollForm)
      },

      {
        path: 'payroll/:id',
        loadComponent: () =>
          import('./features/payroll/payroll-details/payroll-details')
            .then(component => component.PayrollDetails)
      },
       {
        path: 'organization',
        loadChildren: () =>
          import('./features/organization/organization.routes')
            .then(m => m.ORGANIZATION_ROUTES)
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
