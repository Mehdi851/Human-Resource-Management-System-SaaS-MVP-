export interface OrganizationModel {
  id: number;
  name: string;
  code: string;
  email: string;
  phone: string;
  website?: string;

  address: string;
  city: string;
  country: string;

  employeeCount: number;
  departmentCount: number;

  status: OrganizationStatus;

  createdAt: string;
}

export type OrganizationStatus = 'Active' | 'Inactive';