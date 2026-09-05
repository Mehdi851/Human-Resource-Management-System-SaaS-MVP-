export type DesignationStatus = 'Active' | 'Inactive';

export interface DesignationModel {
  id: number;
  name: string;
  description: string;
  department: string;
  employeeCount: number;
  status: DesignationStatus;
  createdAt: string;
  updatedAt: string;
}