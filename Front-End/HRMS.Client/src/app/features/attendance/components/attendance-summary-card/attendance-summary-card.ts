import { Component, input } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-attendance-summary-card',
  styleUrl: './attendance-summary-card.scss',
  templateUrl: './attendance-summary-card.html',
})
export class AttendanceSummaryCard {
  readonly title = input.required<string>();
  readonly value = input.required<number>();
  readonly description = input<string>('');
  readonly icon = input<string>('');
}
