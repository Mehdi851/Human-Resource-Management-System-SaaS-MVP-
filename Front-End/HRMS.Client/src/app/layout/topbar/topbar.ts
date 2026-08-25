import {
  ChangeDetectionStrategy,
  Component,output
} from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { MatMenuModule } from '@angular/material/menu';

@Component({
  imports: [MatIconModule, MatMenuModule],
  standalone: true,
  selector: 'app-topbar',
  styleUrl: './topbar.scss',
  templateUrl: './topbar.html',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class TopbarComponent {
   readonly sidebarToggle = output<void>();

  onSidebarToggle(): void {
    this.sidebarToggle.emit();
  }
}
