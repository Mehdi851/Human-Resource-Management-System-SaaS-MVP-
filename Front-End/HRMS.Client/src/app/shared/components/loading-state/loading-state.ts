import { ChangeDetectionStrategy, Component, input } from '@angular/core';

@Component({
  imports: [],
  selector: 'app-loading-state',
  styleUrl: './loading-state.scss',
  templateUrl: './loading-state.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class LoadingStateComponent {
  readonly message = input<string>('Loading...');
}
