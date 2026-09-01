import { ChangeDetectionStrategy, Component } from '@angular/core';
import { RouterLink } from '@angular/router';
import { StatCard } from './components/stat-card/stat-card';

@Component({
  imports: [StatCard, RouterLink],
  standalone: true,
  selector: 'app-dashboard',
  styleUrl: './dashboard.scss',
  templateUrl: './dashboard.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Dashboard {
  readonly stats = [
  {
    label: 'Total Employees',
    value: '248',
    trend: '↑ 8 this month',
    trendDirection: 'positive' as const,
    supportingText: '',
    icon: '👥',
  },
  {
    label: 'Present Today',
    value: '221',
    trend: '',
    trendDirection: 'neutral' as const,
    supportingText: '89.1% attendance',
    icon: '✓',
  },
  {
    label: 'On Leave',
    value: '12',
    trend: '',
    trendDirection: 'neutral' as const,
    supportingText: 'Today',
    icon: '◷',
  },
  {
    label: 'Payroll',
    value: 'PKR 4.82M',
    trend: '',
    trendDirection: 'neutral' as const,
    supportingText: 'Current month',
    icon: '₨',
  },
] as const;

readonly attendanceSummary = [
  {
    label: 'Present',
    value: 221,
    percentage: 89.1,
  },
  {
    label: 'Late',
    value: 14,
    percentage: 5.6,
  },
  {
    label: 'Absent',
    value: 9,
    percentage: 3.6,
  },
  {
    label: 'On Leave',
    value: 12,
    percentage: 4.8,
  },
] as const;

readonly leaveSummary = [
  {
    label: 'Pending',
    value: 7,
  },
  {
    label: 'Approved',
    value: 18,
  },
  {
    label: 'Rejected',
    value: 3,
  },
] as const;

readonly employeeDistribution = [
  {
    department: 'Engineering',
    employees: 86,
    percentage: 34.7,
  },
  {
    department: 'Operations',
    employees: 54,
    percentage: 21.8,
  },
  {
    department: 'Human Resources',
    employees: 42,
    percentage: 16.9,
  },
  {
    department: 'Sales',
    employees: 35,
    percentage: 14.1,
  },
  {
    department: 'Finance',
    employees: 31,
    percentage: 12.5,
  },
] as const;

readonly payrollSummary = {
  totalPayroll: 'PKR 4.82M',
  processedPercentage: 94,
  employeesPaid: 234,
  totalEmployees: 248,
} as const;

readonly recentActivities = [
  {
    title: 'New employee added',
    description: 'Ali Raza joined the Engineering department.',
    time: '10 minutes ago',
    type: 'employee',
  },
  {
    title: 'Leave request submitted',
    description: 'Sara Ahmed submitted a leave request.',
    time: '35 minutes ago',
    type: 'leave',
  },
  {
    title: 'Attendance updated',
    description: 'Daily attendance records were updated.',
    time: '1 hour ago',
    type: 'attendance',
  },
  {
    title: 'Department updated',
    description: 'Engineering department details were modified.',
    time: '2 hours ago',
    type: 'department',
  },
] as const;

readonly quickActions = [
  {
    title: 'Add Employee',
    description: 'Create a new employee record.',
    route: '/employees/create',
    icon: '+',
  },
  {
    title: 'Manage Departments',
    description: 'View and manage departments.',
    route: '/departments',
    icon: '▦',
  },
  {
    title: 'Review Leave',
    description: 'Review pending leave requests.',
    route: '/leave',
    icon: '◷',
  },
  {
    title: 'View Payroll',
    description: 'Review current payroll information.',
    route: '/payroll',
    icon: 'Rs',
  },
] as const;
}
