export type DepartmentStatus =
  | 'Active'
  | 'Inactive';

export interface DepartmentModel {
  id: number;
  name: string;
  description: string;
  manager: string;
  employeeCount: number;
  status: DepartmentStatus;
  createdAt: string;
  updatedAt: string;
}