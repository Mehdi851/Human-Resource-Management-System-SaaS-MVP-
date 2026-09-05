import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SettingsSidebar } from '../settings-sidebar/settings-sidebar';

@Component({
  selector: 'app-settings-layout',
  standalone: true,
  imports: [
    RouterOutlet,
    SettingsSidebar,
  ],
  templateUrl: './settings-layout.html',
  styleUrl: './settings-layout.scss',
})
export class SettingsLayout {}