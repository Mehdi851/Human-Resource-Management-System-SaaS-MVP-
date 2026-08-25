import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';

@Component({
  imports: [],
  standalone: true,
  selector: 'app-empty-state',
  styleUrl: './empty-state.scss',
  templateUrl: './empty-state.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class EmptyStateComponent{
    readonly title = input<string>('No records found');
    readonly message = input<string>(
      'There are no records to display right now.'
    );
    readonly actionLabel = input<string>('');

    readonly action = output<void>();
}
