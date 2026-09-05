import { SettingsConfiguration } from '../models/settings.model';

export const SETTINGS_CONFIGURATION: SettingsConfiguration = {
  general: {
    organizationName: 'JM Technologies',
    contactEmail: 'admin@jmtechnologies.com',
    phone: '+92 300 1234567',
    address: 'Lahore, Pakistan',
    timezone: 'Asia/Karachi',
    dateFormat: 'DD/MM/YYYY',
    currency: 'PKR',
  },

  work: {
    workingDays: [
      'Monday',
      'Tuesday',
      'Wednesday',
      'Thursday',
      'Friday',
    ],
    workStartTime: '09:00',
    workEndTime: '17:00',
    gracePeriodMinutes: 15,
    enableLateTracking: true,
  },

  leave: {
    leaveYear: 2026,
    requireApproval: true,
    allowCarryForward: true,
    maximumCarryForwardDays: 5,
  },

  payroll: {
    payFrequency: 'Monthly',
    currency: 'PKR',
    payDay: 25,
  },

  notifications: {
    emailNotifications: true,
    leaveNotifications: true,
    attendanceNotifications: true,
    payrollNotifications: true,
  },

  security: {
  enforceStrongPassword: true,
  enableLoginNotifications: true,
},

  system: {
    language: 'English',
    theme: 'light',
    defaultDashboard: 'HR Dashboard',
  },
};