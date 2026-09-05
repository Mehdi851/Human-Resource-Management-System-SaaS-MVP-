import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-employee-list',
  styleUrl: './employee-list.scss',
  templateUrl: './employee-list.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmployeeList {}
