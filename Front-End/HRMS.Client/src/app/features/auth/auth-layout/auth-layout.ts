import {
  ChangeDetectionStrategy,
  Component,
} from '@angular/core';
import { RouterOutlet } from '@angular/router';
@Component({
  imports: [RouterOutlet],
  standalone: true,
  selector: 'app-auth-layout',
  styleUrl: './auth-layout.scss',
  templateUrl: './auth-layout.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AuthLayout {}
