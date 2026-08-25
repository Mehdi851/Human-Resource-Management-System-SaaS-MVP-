import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  imports: [],
  standalone: true,
  selector: 'app-dashboard',
  styleUrl: './dashboard.scss',
  templateUrl: './dashboard.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Dashboard {}
