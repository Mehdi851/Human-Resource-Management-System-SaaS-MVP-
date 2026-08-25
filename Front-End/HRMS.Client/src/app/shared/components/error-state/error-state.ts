import { ChangeDetectionStrategy, Component, input, output } from '@angular/core';

@Component({
  imports: [],
  standalone: true,
  selector: 'app-error-state',
  styleUrl: './error-state.scss',
  templateUrl: './error-state.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class ErrorStateComponent {
    readonly title = input<string>('Something went wrong');
    readonly message = input<string>(
      'We were unable to complete this request. Please try again.'
    );
    readonly actionLabel = input<string>('Try Again');

    readonly retry = output<void>();
}
