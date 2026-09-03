import { Component, Input } from '@angular/core';

import { LeaveType } from '../../models/leave.model';

@Component({
  imports: [],
  selector: 'app-leave-type-badge',
  styleUrl: './leave-type-badge.scss',
  templateUrl: './leave-type-badge.html',
})
export class LeaveTypeBadge {
   @Input({ required: true })
  type!: LeaveType;
}
