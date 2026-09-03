import { Component, computed, inject } from '@angular/core';
import { Location } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { PayrollModel } from '../models/payroll.model';
import { PAYROLL_MOCK_DATA } from '../data/payroll.mock';

@Component({
  imports: [RouterLink],
  selector: 'app-payroll-details',
  styleUrl: './payroll-details.scss',
  templateUrl: './payroll-details.html',
})
export class PayrollDetails {
   private readonly route = inject(ActivatedRoute);
  private readonly location = inject(Location);

  readonly payrollId = Number(
    this.route.snapshot.paramMap.get('id')
  );

  readonly payroll = computed<PayrollModel | undefined>(() =>
    PAYROLL_MOCK_DATA.find(record => record.id === this.payrollId)
  );

  goBack(): void {
    this.location.back();
  }

  getStatusClass(status: PayrollModel['status']): string {
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

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      maximumFractionDigits: 0
    }).format(value);
  }
}
