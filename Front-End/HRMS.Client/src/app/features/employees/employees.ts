import {ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  imports: [],
  standalone: true,
  selector: 'app-employees',
  styleUrl: './employees.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './employees.html',
})
export class Employees {}
