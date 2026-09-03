import {
  ChangeDetectionStrategy,
  Component,
  input,
  output
} from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';
import { MatIconModule } from '@angular/material/icon';

import { MatTooltipModule } from '@angular/material/tooltip';


interface NavigationItem {
  readonly label: string;
  readonly icon: string;
  readonly route: string;
}
@Component({
  imports: [RouterLink, RouterLinkActive, MatIconModule,MatTooltipModule],
  standalone: true,
  selector: 'app-sidebar',
  styleUrl: './sidebar.scss',
  templateUrl: './sidebar.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class SidebarComponent {
  readonly collapsed = input(false);
  readonly mobileOpen = input(false);

  readonly navigationSelected = output<void>();

   readonly navigationItems: readonly NavigationItem[] = [
    {
      label: 'Dashboard',
      icon: 'dashboard',
      route: '/dashboard'
    },
    {
      label: 'Employees',
      icon: 'people',
      route: '/employees'
    },
    {
      label: 'Departments',
      icon: 'business',
      route: '/departments'
    },
    {
      label: 'Designations',
      icon: 'work',
      route: '/designations'
    },
    {
      label: 'Attendance',
      icon: 'schedule',
      route: '/attendance'
    },
    {
      label: 'Leaves',
      icon: 'event',
      route: '/leaves'
    },
    {
      label: 'Payroll',
      icon: 'payments',
      route: '/payroll'
    },
    {
      label: 'Organization',
      icon: 'settings',
      route: '/organization'
    }
  ];

  onNavigationSelected(): void {
    this.navigationSelected.emit();
  }
}
