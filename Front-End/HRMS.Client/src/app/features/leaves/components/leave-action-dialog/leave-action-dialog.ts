import {Component,EventEmitter,Input,Output} from '@angular/core';
import {FormsModule} from '@angular/forms';

export type LeaveAction = 'approve' | 'reject';

@Component({
  imports: [FormsModule],
  selector: 'app-leave-action-dialog',
  styleUrl: './leave-action-dialog.scss',
  templateUrl: './leave-action-dialog.html',
})
export class LeaveActionDialog {
  @Input({ required: true })
  action!: LeaveAction;

  @Input()
  employeeName = '';

  @Output()
  confirmed = new EventEmitter<string>();

  @Output()
  cancelled = new EventEmitter<void>();

  rejectionReason = '';

  get isRejectAction(): boolean {
    return this.action === 'reject';
  }

  get title(): string {
    return this.isRejectAction
      ? 'Reject Leave Request?'
      : 'Approve Leave Request?';
  }

  get description(): string {
    return this.isRejectAction
      ? `Please provide a reason for rejecting ${this.employeeName}'s leave request.`
      : `Are you sure you want to approve ${this.employeeName}'s leave request?`;
  }

  get actionLabel(): string {
    return this.isRejectAction
      ? 'Reject Request'
      : 'Approve Request';
  }

  confirm(): void {

    if (
      this.isRejectAction &&
      !this.rejectionReason.trim()
    ) {
      return;
    }

    this.confirmed.emit(
      this.rejectionReason.trim()
    );
  }

  cancel(): void {
    this.cancelled.emit();
  }
}
