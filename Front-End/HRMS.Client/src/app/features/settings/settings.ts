import { Component, signal } from '@angular/core';
import { SettingsLayout } from './components/settings-layout/settings-layout';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [
    SettingsLayout,
  ],
  templateUrl: './settings.html',
  styleUrl: './settings.scss',
})
export class Settings {

  readonly pageTitle = signal('Settings');

  readonly pageDescription = signal(
    'Manage your organization and application preferences.'
  );
}