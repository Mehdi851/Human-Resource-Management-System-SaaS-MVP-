import {
  ChangeDetectionStrategy,
  Component,
  input,
} from '@angular/core';

import { EmploymentStatus } from '../../models/employee.model';

@Component({
  imports: [],
  selector: 'app-employee-status-badge',
  styleUrl: './employee-status-badge.scss',
  templateUrl: './employee-status-badge.html',
})
export class EmployeeStatusBadge {
  readonly status = input.required<EmploymentStatus>();
}
