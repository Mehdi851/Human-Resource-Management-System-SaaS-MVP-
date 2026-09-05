export interface GeneralSettings {
  organizationName: string;
  contactEmail: string;
  phone: string;
  address: string;
  timezone: string;
  dateFormat: string;
  currency: string;
}

export interface WorkSettings {
  workingDays: string[];
  workStartTime: string;
  workEndTime: string;
  gracePeriodMinutes: number;
  enableLateTracking: boolean;
}

export interface LeaveSettings {
  leaveYear: number;
  requireApproval: boolean;
  allowCarryForward: boolean;
  maximumCarryForwardDays: number;
}

export interface PayrollSettings {
  payFrequency: string;
  currency: string;
  payDay: number;
}

export interface NotificationSettings {
  emailNotifications: boolean;
  leaveNotifications: boolean;
  attendanceNotifications: boolean;
  payrollNotifications: boolean;
}

export interface SecuritySettings {
  enforceStrongPassword: boolean;
  enableLoginNotifications: boolean;
}

export interface SystemSettings {
  language: string;
  theme: 'light' | 'dark' | 'system';
  defaultDashboard: string;
}

export interface SettingsConfiguration {
  general: GeneralSettings;
  work: WorkSettings;
  leave: LeaveSettings;
  payroll: PayrollSettings;
  notifications: NotificationSettings;
  security: SecuritySettings;
  system: SystemSettings;
}