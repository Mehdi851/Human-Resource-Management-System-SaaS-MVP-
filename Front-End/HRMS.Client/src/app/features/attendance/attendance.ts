import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  imports: [],
  standalone: true,
  selector: 'app-attendance',
  styleUrl: './attendance.scss',
  templateUrl: './attendance.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Attendance {}
