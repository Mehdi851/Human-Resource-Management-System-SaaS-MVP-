import { AttendanceModel } from '../models/attendance.model';

export const ATTENDANCE_RECORDS: AttendanceModel[] = [
  {
    id: 1,
    employeeId: 'EMP-001',
    employeeName: 'Ahmed Khan',
    department: 'Engineering',
    date: '2026-09-03',
    checkIn: '08:52',
    checkOut: '17:15',
    workingHours: '8h 23m',
    status: 'Present',
    remarks: ''
  },
  {
    id: 2,
    employeeId: 'EMP-002',
    employeeName: 'Sara Ahmed',
    department: 'HR',
    date: '2026-09-03',
    checkIn: '09:18',
    checkOut: '17:05',
    workingHours: '7h 47m',
    status: 'Late',
    remarks: 'Traffic delay'
  },
  {
    id: 3,
    employeeId: 'EMP-003',
    employeeName: 'Usman Ali',
    department: 'Finance',
    date: '2026-09-03',
    checkIn: null,
    checkOut: null,
    workingHours: '0h 00m',
    status: 'Absent',
    remarks: ''
  },
  {
    id: 4,
    employeeId: 'EMP-004',
    employeeName: 'Fatima Noor',
    department: 'Sales',
    date: '2026-09-03',
    checkIn: '09:02',
    checkOut: '13:32',
    workingHours: '4h 30m',
    status: 'Half Day',
    remarks: 'Personal appointment'
  },
  {
    id: 5,
    employeeId: 'EMP-005',
    employeeName: 'Bilal Hassan',
    department: 'Operations',
    date: '2026-09-03',
    checkIn: null,
    checkOut: null,
    workingHours: '0h 00m',
    status: 'On Leave',
    remarks: 'Approved annual leave'
  },
  {
    id: 6,
    employeeId: 'EMP-006',
    employeeName: 'Ayesha Malik',
    department: 'Engineering',
    date: '2026-09-03',
    checkIn: '08:47',
    checkOut: '17:12',
    workingHours: '8h 25m',
    status: 'Present',
    remarks: ''
  },
  {
    id: 7,
    employeeId: 'EMP-007',
    employeeName: 'Hamza Raza',
    department: 'Sales',
    date: '2026-09-02',
    checkIn: '09:21',
    checkOut: '17:02',
    workingHours: '7h 41m',
    status: 'Late',
    remarks: 'Late arrival'
  },
  {
    id: 8,
    employeeId: 'EMP-008',
    employeeName: 'Mariam Iqbal',
    department: 'HR',
    date: '2026-09-02',
    checkIn: '08:58',
    checkOut: '17:10',
    workingHours: '8h 12m',
    status: 'Present',
    remarks: ''
  }
];