import { ChangeDetectionStrategy, Component, input } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-status-badge',
  styleUrl: './status-badge.scss',
  templateUrl: './status-badge.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class StatusBadgeComponent {
   readonly status = input.required<string>();
}
