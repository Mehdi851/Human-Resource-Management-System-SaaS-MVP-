import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive } from '@angular/router';

interface SettingsNavigationItem {
  label: string;
  route: string;
  icon: string;
}

@Component({
  selector: 'app-settings-sidebar',
  standalone: true,
  imports: [
    RouterLink,
    RouterLinkActive,
  ],
  templateUrl: './settings-sidebar.html',
  styleUrl: './settings-sidebar.scss',
})
export class SettingsSidebar {

  readonly navigationItems: SettingsNavigationItem[] = [
    {
      label: 'General',
      route: 'general',
      icon: 'bi-building',
    },
    {
      label: 'Work & Attendance',
      route: 'work',
      icon: 'bi-clock',
    },
    {
      label: 'Leave',
      route: 'leave',
      icon: 'bi-calendar-check',
    },
    {
      label: 'Payroll',
      route: 'payroll',
      icon: 'bi-cash-stack',
    },
    {
      label: 'Notifications',
      route: 'notifications',
      icon: 'bi-bell',
    },
    {
      label: 'Security',
      route: 'security',
      icon: 'bi-shield-lock',
    },
    {
      label: 'System',
      route: 'system',
      icon: 'bi-sliders',
    },
  ];
}