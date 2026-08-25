import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  imports: [],
  standalone: true,
  selector: 'app-leave',
  styleUrl: './leave.scss',
  templateUrl: './leave.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Leave {}
