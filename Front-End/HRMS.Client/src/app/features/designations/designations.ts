import { ChangeDetectionStrategy,Component } from '@angular/core';

@Component({
  imports: [],
  standalone: true,
  selector: 'app-designations',
  styleUrl: './designations.scss',
  templateUrl: './designations.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Designations {}
