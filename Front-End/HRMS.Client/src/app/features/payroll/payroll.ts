import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  imports: [],
  standalone: true,
  selector: 'app-payroll',
  styleUrl: './payroll.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './payroll.html',
})
export class Payroll {}
