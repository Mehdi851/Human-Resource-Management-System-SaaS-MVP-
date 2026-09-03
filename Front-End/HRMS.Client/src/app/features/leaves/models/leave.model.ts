export type LeaveStatus =
  | 'Pending'
  | 'Approved'
  | 'Rejected'
  | 'Cancelled';

export type LeaveType =
  | 'Annual'
  | 'Sick'
  | 'Casual'
  | 'Unpaid'
  | 'Maternity'
  | 'Paternity';

export interface LeaveRequest {
  id: number;

  employeeId: number;
  employeeCode: string;
  employeeName: string;

  department: string;
  designation: string;

  leaveType: LeaveType;

  startDate: string;
  endDate: string;

  duration: number;

  reason: string;

  status: LeaveStatus;

  submittedDate: string;

  rejectionReason?: string;
}