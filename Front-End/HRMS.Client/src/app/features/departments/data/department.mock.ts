import { DepartmentModel } from '../models/department.model';

export const DEPARTMENT_RECORDS: DepartmentModel[] = [
  {
    id: 1,
    name: 'Engineering',
    description: 'Software development, engineering, and technical operations.',
    manager: 'Ahmed Khan',
    employeeCount: 24,
    status: 'Active',
    createdAt: '2025-01-15',
    updatedAt: '2026-08-20'
  },
  {
    id: 2,
    name: 'Human Resources',
    description: 'Employee relations, recruitment, policies, and workforce management.',
    manager: 'Sara Ahmed',
    employeeCount: 8,
    status: 'Active',
    createdAt: '2025-01-20',
    updatedAt: '2026-08-18'
  },
  {
    id: 3,
    name: 'Finance',
    description: 'Financial planning, accounting, budgeting, and reporting.',
    manager: 'Usman Ali',
    employeeCount: 7,
    status: 'Active',
    createdAt: '2025-02-05',
    updatedAt: '2026-08-15'
  },
  {
    id: 4,
    name: 'Sales',
    description: 'Sales operations, customer acquisition, and revenue growth.',
    manager: 'Fatima Noor',
    employeeCount: 15,
    status: 'Active',
    createdAt: '2025-02-12',
    updatedAt: '2026-08-22'
  },
  {
    id: 5,
    name: 'Operations',
    description: 'Business operations, process management, and administrative activities.',
    manager: 'Bilal Hassan',
    employeeCount: 11,
    status: 'Active',
    createdAt: '2025-03-01',
    updatedAt: '2026-08-19'
  },
  {
    id: 6,
    name: 'Marketing',
    description: 'Marketing strategy, branding, campaigns, and digital engagement.',
    manager: 'Ayesha Malik',
    employeeCount: 6,
    status: 'Inactive',
    createdAt: '2025-03-10',
    updatedAt: '2026-07-30'
  }
];