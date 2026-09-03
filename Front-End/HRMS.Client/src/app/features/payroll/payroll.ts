import { ChangeDetectionStrategy, Component, computed, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { PayrollModel, PayrollStatus } from './models/payroll.model';
import { PAYROLL_MOCK_DATA } from './data/payroll.mock';

@Component({
  imports: [RouterLink],
  standalone: true,
  selector: 'app-payroll',
  styleUrl: './payroll.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './payroll.html',
})
export class Payroll {
   readonly payrollRecords = signal<PayrollModel[]>(PAYROLL_MOCK_DATA);

  readonly searchTerm = signal('');
  readonly selectedDepartment = signal('All Departments');
  readonly selectedStatus = signal('All Statuses');
  readonly selectedPeriod = signal('September 2026');

  readonly departments = computed(() => {
    const values = this.payrollRecords()
      .map(record => record.department)
      .filter((department, index, departments) =>
        departments.indexOf(department) === index
      );

    return ['All Departments', ...values];
  });

  readonly statuses: PayrollStatus[] = [
    'Draft',
    'Processing',
    'Processed',
    'Paid',
    'On Hold'
  ];
  readonly workflowSteps = [
    'Draft',
    'Processing',
    'Processed',
    'Paid'
  ] as const;

  readonly currentWorkflowStatus = signal<PayrollStatus>('Processing');
  readonly filteredPayroll = computed(() => {
    const search = this.searchTerm().trim().toLowerCase();
    const department = this.selectedDepartment();
    const status = this.selectedStatus();
    const period = this.selectedPeriod();

    return this.payrollRecords().filter(record => {
      const matchesSearch =
        !search ||
        record.employeeName.toLowerCase().includes(search) ||
        record.employeeId.toLowerCase().includes(search);

      const matchesDepartment =
        department === 'All Departments' ||
        record.department === department;

      const matchesStatus =
        status === 'All Statuses' ||
        record.status === status;

      const matchesPeriod =
        record.period === period;

      return (
        matchesSearch &&
        matchesDepartment &&
        matchesStatus &&
        matchesPeriod
      );
    });
  });

  readonly currentPeriod = computed(() => this.selectedPeriod());

  readonly totalEmployees = computed(() => {
    return this.filteredPayroll().length;
  });

  readonly grossPayroll = computed(() => {
    return this.filteredPayroll().reduce(
      (total, record) => total + record.grossSalary,
      0
    );
  });

  readonly netPayroll = computed(() => {
    return this.filteredPayroll().reduce(
      (total, record) => total + record.netSalary,
      0
    );
  });

  setSearchTerm(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.searchTerm.set(input.value);
  }

  setDepartment(event: Event): void {
    const select = event.target as HTMLSelectElement;
    this.selectedDepartment.set(select.value);
  }

  setStatus(event: Event): void {
    const select = event.target as HTMLSelectElement;
    this.selectedStatus.set(select.value);
  }

  setPeriod(event: Event): void {
    const select = event.target as HTMLSelectElement;
    this.selectedPeriod.set(select.value);
  }

  getStatusClass(status: PayrollStatus): string {
    switch (status) {
      case 'Processed':
      case 'Paid':
        return 'status-success';

      case 'Processing':
        return 'status-warning';

      case 'On Hold':
        return 'status-danger';

      case 'Draft':
      default:
        return 'status-neutral';
    }
  }
  getWorkflowStepClass(
    step: (typeof this.workflowSteps)[number]
  ): string {
    const currentIndex = this.workflowSteps.indexOf(
      this.currentWorkflowStatus() as (typeof this.workflowSteps)[number]
    );

    const stepIndex = this.workflowSteps.indexOf(step);

    if (stepIndex < currentIndex) {
      return 'workflow-complete';
    }

    if (stepIndex === currentIndex) {
      return 'workflow-current';
    }

    return 'workflow-upcoming';
  }

  processPayroll(): void {
    this.currentWorkflowStatus.set('Processing');
  }
  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      maximumFractionDigits: 0
    }).format(value);
  }
}
