import { ChangeDetectionStrategy, Component,signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SidebarComponent } from '../sidebar/sidebar';

import { TopbarComponent } from '../topbar/topbar';

@Component({
  imports: [RouterOutlet, SidebarComponent, TopbarComponent],
  selector: 'app-shell',
  standalone: true,
  styleUrl: './app-shell.scss',
  templateUrl: './app-shell.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class AppShellComponent {
  readonly sidebarCollapsed = signal(false);
  readonly mobileSidebarOpen = signal(false);

  toggleSidebar(): void {
    if (this.isMobileViewport()) {
      this.mobileSidebarOpen.update(open => !open);
      return;
    }

    this.sidebarCollapsed.update(collapsed => !collapsed);
  }

  closeMobileSidebar(): void {
    this.mobileSidebarOpen.set(false);
  }

  private isMobileViewport(): boolean {
    return window.innerWidth < 992;
  }
}
