import { Component, computed, inject } from '@angular/core';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { PAYROLL_MOCK_DATA } from '../data/payroll.mock';
import { PayrollModel, PayrollStatus } from '../models/payroll.model';
import { Payroll } from '../payroll';

@Component({
  imports: [ReactiveFormsModule, RouterLink],
  selector: 'app-payroll-form',
  styleUrl: './payroll-form.scss',
  templateUrl: './payroll-form.html',
})
export class PayrollForm {
   private readonly formBuilder = inject(FormBuilder);
  private readonly route = inject(ActivatedRoute);
  private readonly router = inject(Router);

  readonly payrollId = Number(
    this.route.snapshot.paramMap.get('id')
  );

  readonly isEditMode = this.route.snapshot.routeConfig?.path === ':id/edit';

  readonly payroll = computed<PayrollModel | undefined>(() =>
    PAYROLL_MOCK_DATA.find(record => record.id === this.payrollId)
  );

  readonly statuses: PayrollStatus[] = [
    'Draft',
    'Processing',
    'Processed',
    'Paid',
    'On Hold'
  ];

  readonly employees = PAYROLL_MOCK_DATA.filter(
    (record, index, records) =>
      records.findIndex(
        item => item.employeeId === record.employeeId
      ) === index
  );

  readonly payrollForm = this.formBuilder.nonNullable.group({
    employeeId: ['', Validators.required],
    period: ['September 2026', Validators.required],
    basicSalary: [0, [Validators.required, Validators.min(0)]],
    housingAllowance: [0, [Validators.min(0)]],
    transportAllowance: [0, [Validators.min(0)]],
    otherAllowances: [0, [Validators.min(0)]],
    taxDeduction: [0, [Validators.min(0)]],
    insuranceDeduction: [0, [Validators.min(0)]],
    otherDeductions: [0, [Validators.min(0)]],
    status: ['Draft' as PayrollStatus, Validators.required],
    remarks: ['']
  });

  constructor() {
    if (this.isEditMode) {
      const record = this.payroll();

      if (record) {
        this.payrollForm.patchValue({
          employeeId: record.employeeId,
          period: record.period,
          basicSalary: record.basicSalary,
          housingAllowance: this.getHousingAllowance(record),
          transportAllowance: this.getTransportAllowance(record),
          otherAllowances: this.getOtherAllowances(record),
          taxDeduction: this.getTaxDeduction(record),
          insuranceDeduction: this.getInsuranceDeduction(record),
          otherDeductions: this.getOtherDeductions(record),
          status: record.status,
          remarks: record.remarks ?? ''
        });
      }
    }
  }

  readonly selectedEmployee = computed(() =>
    this.employees.find(
      employee => employee.employeeId === this.payrollForm.controls.employeeId.value
    )
  );

  readonly grossSalary = computed(() => {
    const basic = Number(this.payrollForm.controls.basicSalary.value);
    const housing = Number(
      this.payrollForm.controls.housingAllowance.value
    );
    const transport = Number(
      this.payrollForm.controls.transportAllowance.value
    );
    const other = Number(
      this.payrollForm.controls.otherAllowances.value
    );

    return basic + housing + transport + other;
  });

  readonly totalDeductions = computed(() => {
    const tax = Number(this.payrollForm.controls.taxDeduction.value);
    const insurance = Number(
      this.payrollForm.controls.insuranceDeduction.value
    );
    const other = Number(
      this.payrollForm.controls.otherDeductions.value
    );

    return tax + insurance + other;
  });

  readonly netSalary = computed(() =>
    Math.max(0, this.grossSalary() - this.totalDeductions())
  );

  onSubmit(): void {
    if (this.payrollForm.invalid) {
      this.payrollForm.markAllAsTouched();
      return;
    }

    if (this.isEditMode) {
      this.router.navigate(['/payroll', this.payrollId]);
      return;
    }

    this.router.navigate(['/payroll']);
  }

  formatCurrency(value: number): string {
    return new Intl.NumberFormat('en-US', {
      style: 'currency',
      currency: 'USD',
      maximumFractionDigits: 0
    }).format(value);
  }

  private getHousingAllowance(record: PayrollModel): number {
    return Math.round(record.allowances * 0.5);
  }

  private getTransportAllowance(record: PayrollModel): number {
    return Math.round(record.allowances * 0.33);
  }

  private getOtherAllowances(record: PayrollModel): number {
    return Math.max(
      0,
      record.allowances -
        this.getHousingAllowance(record) -
        this.getTransportAllowance(record)
    );
  }

  private getTaxDeduction(record: PayrollModel): number {
    return Math.round(record.deductions * 0.71);
  }

  private getInsuranceDeduction(record: PayrollModel): number {
    return Math.round(record.deductions * 0.14);
  }

  private getOtherDeductions(record: PayrollModel): number {
    return Math.max(
      0,
      record.deductions -
        this.getTaxDeduction(record) -
        this.getInsuranceDeduction(record)
    );
  }
}
