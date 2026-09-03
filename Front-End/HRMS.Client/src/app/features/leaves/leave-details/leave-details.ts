import { Component, computed, inject, signal } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { LeaveStatusBadge } from '../components/leave-status-badge/leave-status-badge';

import { LeaveRequest } from '../models/leave.model';
import { LEAVE_REQUESTS } from '../data/leave.mock';
import {LeaveAction,LeaveActionDialog} from '../components/leave-action-dialog/leave-action-dialog';
@Component({
  imports: [RouterLink,LeaveActionDialog, LeaveStatusBadge],
  selector: 'app-leave-details',
  styleUrl: './leave-details.scss',
  templateUrl: './leave-details.html',
})
export class LeaveDetails {
  private readonly route = inject(ActivatedRoute);
  readonly activeAction = signal<LeaveAction | null>(null);

  readonly showActionDialog = computed(() =>
    this.activeAction() !== null
  );
  readonly leaveId = computed(() =>
    Number(this.route.snapshot.paramMap.get('id'))
  );

  readonly leaveRequest = computed<LeaveRequest | undefined>(() =>
    LEAVE_REQUESTS.find(
      leave => leave.id === this.leaveId()
    )
  );

  readonly isNotFound = computed(() =>
    !this.leaveRequest()
  );

  openAction(action: LeaveAction): void {
  this.activeAction.set(action);
}

closeAction(): void {
  this.activeAction.set(null);
}

confirmAction(reason: string): void {

  const leave = this.leaveRequest();

  if (!leave) {
    return;
  }

  if (this.activeAction() === 'approve') {

    console.log(
      'Approve leave request',
      leave.id
    );

  } else if (this.activeAction() === 'reject') {

    console.log(
      'Reject leave request',
      leave.id,
      reason
    );

  }

  this.activeAction.set(null);
}
}
