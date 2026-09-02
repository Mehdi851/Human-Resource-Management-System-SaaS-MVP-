export type EmploymentStatus = 'Active' | 'Inactive' | 'On Leave';

export interface Employee {
  id: string;
  employeeCode: string;

  firstName: string;
  lastName: string;

  email: string;
  phone: string;

  department: string;
  designation: string;

  joiningDate: string;

  employmentStatus: EmploymentStatus;

  avatarUrl?: string;
}