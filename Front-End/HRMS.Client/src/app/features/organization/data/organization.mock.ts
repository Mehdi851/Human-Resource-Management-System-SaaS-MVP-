import { OrganizationModel } from '../models/organization.model';

export const ORGANIZATION_MOCK_DATA: OrganizationModel[] = [
  {
    id: 1,
    name: 'TechVision Solutions',
    code: 'TVS',
    email: 'info@techvision.com',
    phone: '+92 300 1234567',
    website: 'https://www.techvision.com',

    address: 'Gulberg III',
    city: 'Lahore',
    country: 'Pakistan',

    employeeCount: 85,
    departmentCount: 8,

    status: 'Active',

    createdAt: '2026-01-15'
  },
  {
    id: 2,
    name: 'Nexus Software Labs',
    code: 'NSL',
    email: 'info@nexuslabs.com',
    phone: '+92 301 9876543',
    website: 'https://www.nexuslabs.com',

    address: 'Blue Area',
    city: 'Islamabad',
    country: 'Pakistan',

    employeeCount: 52,
    departmentCount: 6,

    status: 'Active',

    createdAt: '2026-02-03'
  },
  {
    id: 3,
    name: 'Global Tech Systems',
    code: 'GTS',
    email: 'contact@globaltech.com',
    phone: '+92 302 4567890',
    website: 'https://www.globaltech.com',

    address: 'Shahrah-e-Faisal',
    city: 'Karachi',
    country: 'Pakistan',

    employeeCount: 120,
    departmentCount: 10,

    status: 'Active',

    createdAt: '2026-02-18'
  },
  {
    id: 4,
    name: 'Innovate Digital',
    code: 'IND',
    email: 'hello@innovatedigital.com',
    phone: '+92 303 1122334',

    address: 'DHA Phase 5',
    city: 'Lahore',
    country: 'Pakistan',

    employeeCount: 34,
    departmentCount: 5,

    status: 'Inactive',

    createdAt: '2026-03-01'
  },
  {
    id: 5,
    name: 'CloudMatrix Technologies',
    code: 'CMT',
    email: 'info@cloudmatrix.com',
    phone: '+92 304 5566778',
    website: 'https://www.cloudmatrix.com',

    address: 'Johar Town',
    city: 'Lahore',
    country: 'Pakistan',

    employeeCount: 67,
    departmentCount: 7,

    status: 'Active',

    createdAt: '2026-03-12'
  },
  {
    id: 6,
    name: 'Prime Business Solutions',
    code: 'PBS',
    email: 'contact@primebusiness.com',
    phone: '+92 305 9988776',

    address: 'F-8 Markaz',
    city: 'Islamabad',
    country: 'Pakistan',

    employeeCount: 41,
    departmentCount: 5,

    status: 'Active',

    createdAt: '2026-04-05'
  }
];