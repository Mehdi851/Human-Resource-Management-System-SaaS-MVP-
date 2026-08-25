import { ChangeDetectionStrategy,Component } from '@angular/core';

@Component({
  imports: [],
  standalone: true,
  selector: 'app-departments',
  styleUrl: './departments.scss',
  templateUrl: './departments.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Departments {}
