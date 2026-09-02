import {
  ChangeDetectionStrategy,Component,computed,inject,} from '@angular/core';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { EmployeeStatusBadge } from '../components/employee-status-badge/employee-status-badge';
import { MOCK_EMPLOYEES } from '../data/employee.mock';
import { Employee } from '../models/employee.model';

@Component({
  imports: [DatePipe,
    RouterLink,
    EmployeeStatusBadge,],
  selector: 'app-employee-details',
  styleUrl: './employee-details.scss',
  templateUrl: './employee-details.html',
})
export class EmployeeDetails {
  private readonly route = inject(ActivatedRoute);

  readonly employeeId =
    this.route.snapshot.paramMap.get('id');

  readonly employee = computed<Employee | undefined>(() => {
    return MOCK_EMPLOYEES.find(
      employee =>
        employee.id === this.employeeId,
    );
  });

  readonly initials = computed(() => {
    const employee = this.employee();

    if (!employee) {
      return '';
    }

    return (
      employee.firstName.charAt(0) +
      employee.lastName.charAt(0)
    );
  });

  readonly fullName = computed(() => {
    const employee = this.employee();

    if (!employee) {
      return 'Employee Not Found';
    }

    return `${employee.firstName} ${employee.lastName}`;
  });

}
