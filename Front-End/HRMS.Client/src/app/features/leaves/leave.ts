import { ChangeDetectionStrategy, Component, computed, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import {LeaveAction,LeaveActionDialog} from './components/leave-action-dialog/leave-action-dialog';
import { LeaveStatusBadge } from './components/leave-status-badge/leave-status-badge';
import { LeaveTypeBadge } from './components/leave-type-badge/leave-type-badge';
import {LeaveRequest,LeaveStatus,LeaveType} from './models/leave.model';
import { LEAVE_REQUESTS } from './data/leave.mock';
import { L } from '@angular/cdk/keycodes';
@Component({
  imports: [FormsModule,  RouterLink, LeaveActionDialog, LeaveStatusBadge,LeaveTypeBadge],
  standalone: true,
  selector: 'app-leave',
  styleUrl: './leave.scss',
  templateUrl: './leave.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Leave {
  readonly leaveRequests = signal<LeaveRequest[]>(LEAVE_REQUESTS);

  readonly searchTerm = signal('');
  readonly selectedLeaveType = signal<LeaveType | ''>('');
  readonly selectedStatus = signal<LeaveStatus | ''>('');
  readonly startDate = signal('');
  readonly endDate = signal('');

  readonly leaveTypes: LeaveType[] = [
    'Annual',
    'Sick',
    'Casual',
    'Unpaid',
    'Maternity',
    'Paternity'
  ];

  readonly statuses: LeaveStatus[] = [
    'Pending',
    'Approved',
    'Rejected',
    'Cancelled'
  ];

    readonly activeAction = signal<{
    action: LeaveAction;
    leaveId: number;
    } | null>(null);

    readonly selectedLeaveForAction = computed(() => {

      const action = this.activeAction();

      if (!action) {
        return undefined;
      }

      return this.leaveRequests().find(
        leave => leave.id === action.leaveId
      );
    });
    openAction(
  action: LeaveAction,
  leaveId: number
): void {
  this.activeAction.set({
    action,
    leaveId
  });
}

closeAction(): void {
  this.activeAction.set(null);
}

confirmAction(reason: string): void {

  const action = this.activeAction();

  if (!action) {
    return;
  }

  console.log(
    action.action === 'approve'
      ? 'Approve leave request'
      : 'Reject leave request',
    action.leaveId,
    reason
  );

  this.activeAction.set(null);
}

  readonly pendingCount = computed(() =>
    this.leaveRequests()
      .filter(leave => leave.status === 'Pending')
      .length
  );

  readonly approvedCount = computed(() =>
    this.leaveRequests()
      .filter(leave => leave.status === 'Approved')
      .length
  );

  readonly rejectedCount = computed(() =>
    this.leaveRequests()
      .filter(leave => leave.status === 'Rejected')
      .length
  );

  readonly onLeaveTodayCount = computed(() => {
    const today = new Date().toISOString().split('T')[0];

    return this.leaveRequests().filter(leave =>
      leave.status === 'Approved' &&
      leave.startDate <= today &&
      leave.endDate >= today
    ).length;
  });

  readonly filteredLeaveRequests = computed(() => {
    const search = this.searchTerm().trim().toLowerCase();
    const leaveType = this.selectedLeaveType();
    const status = this.selectedStatus();
    const startDate = this.startDate();
    const endDate = this.endDate();

    return this.leaveRequests().filter(leave => {

      const matchesSearch =
        !search ||
        leave.employeeName.toLowerCase().includes(search) ||
        leave.employeeCode.toLowerCase().includes(search);

      const matchesLeaveType =
        !leaveType ||
        leave.leaveType === leaveType;

      const matchesStatus =
        !status ||
        leave.status === status;

      const matchesStartDate =
        !startDate ||
        leave.startDate >= startDate;

      const matchesEndDate =
        !endDate ||
        leave.endDate <= endDate;

      return (
        matchesSearch &&
        matchesLeaveType &&
        matchesStatus &&
        matchesStartDate &&
        matchesEndDate
      );
    });
  });

  readonly hasActiveFilters = computed(() =>
    !!this.searchTerm() ||
    !!this.selectedLeaveType() ||
    !!this.selectedStatus() ||
    !!this.startDate() ||
    !!this.endDate()
  );

  updateSearch(value: string): void {
    this.searchTerm.set(value);
  }

  updateLeaveType(value: string): void {
    this.selectedLeaveType.set(value as LeaveType | '');
  }

  updateStatus(value: string): void {
    this.selectedStatus.set(value as LeaveStatus | '');
  }

  updateStartDate(value: string): void {
    this.startDate.set(value);
  }

  updateEndDate(value: string): void {
    this.endDate.set(value);
  }

  clearFilters(): void {
    this.searchTerm.set('');
    this.selectedLeaveType.set('');
    this.selectedStatus.set('');
    this.startDate.set('');
    this.endDate.set('');
  }
}
