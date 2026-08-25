import { ChangeDetectionStrategy, Component } from '@angular/core';

@Component({
  imports: [],
  standalone: true,
  selector: 'app-organization',
  styleUrl: './organization.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './organization.html',
})
export class Organization {}
