import { Component, Input } from '@angular/core';
export type LeaveStatus =
  | 'Pending'
  | 'Approved'
  | 'Rejected'
  | 'Cancelled';

@Component({
  imports: [],
  selector: 'app-leave-status-badge',
  styleUrl: './leave-status-badge.scss',
  templateUrl: './leave-status-badge.html',
})
export class LeaveStatusBadge {
   @Input({ required: true })
  status!: LeaveStatus;
}
