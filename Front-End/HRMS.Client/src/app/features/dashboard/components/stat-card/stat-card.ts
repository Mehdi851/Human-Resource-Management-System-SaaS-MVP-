import {ChangeDetectionStrategy,Component,input,} from '@angular/core';

@Component({
  imports: [],
  selector: 'app-stat-card',
  styleUrl: './stat-card.scss',
  templateUrl: './stat-card.html',
})
export class StatCard {
   readonly label = input.required<string>();
  readonly value = input.required<string>();
  readonly supportingText = input<string>('');
  readonly icon = input<string>('');
  readonly trend = input<string>('');
  readonly trendDirection = input<'positive' | 'negative' | 'neutral'>('neutral');
}
