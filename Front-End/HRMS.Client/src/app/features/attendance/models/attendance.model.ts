export type AttendanceStatus =
  | 'Present'
  | 'Absent'
  | 'Late'
  | 'Half Day'
  | 'On Leave';

export interface AttendanceModel {
  id: number;
  employeeId: string;
  employeeName: string;
  department: string;
  date: string;
  checkIn: string | null;
  checkOut: string | null;
  workingHours: string;
  status: AttendanceStatus;
  remarks: string;
}