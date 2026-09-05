import { ChangeDetectionStrategy, Component, computed, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';

import {
  DepartmentModel,
  DepartmentStatus
} from './models/department.model';

import { DEPARTMENT_RECORDS } from './data/department.mock';
import { CommonModule } from '@angular/common';

@Component({
  imports: [CommonModule, FormsModule, RouterLink],
  standalone: true,
  selector: 'app-departments',
  styleUrl: './departments.scss',
  templateUrl: './departments.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Departments {
  readonly departmentRecords = DEPARTMENT_RECORDS;

  readonly searchTerm = signal('');
  readonly selectedStatus = signal<DepartmentStatus | ''>('');

  readonly statuses: DepartmentStatus[] = [
    'Active',
    'Inactive'
  ];

  readonly filteredDepartments = computed<DepartmentModel[]>(() => {
    const search = this.searchTerm().trim().toLowerCase();
    const status = this.selectedStatus();

    return this.departmentRecords.filter(department => {
      const matchesSearch =
        !search ||
        department.name.toLowerCase().includes(search) ||
        department.description.toLowerCase().includes(search) ||
        department.manager.toLowerCase().includes(search);

      const matchesStatus =
        !status ||
        department.status === status;

      return matchesSearch && matchesStatus;
    });
  });

  readonly totalDepartments = computed(
    () => this.departmentRecords.length
  );

  readonly activeDepartments = computed(
    () =>
      this.departmentRecords.filter(
        department => department.status === 'Active'
      ).length
  );

  readonly inactiveDepartments = computed(
    () =>
      this.departmentRecords.filter(
        department => department.status === 'Inactive'
      ).length
  );

  readonly totalEmployees = computed(
    () =>
      this.departmentRecords.reduce(
        (total, department) => total + department.employeeCount,
        0
      )
  );

  updateSearch(value: string): void {
    this.searchTerm.set(value);
  }

  updateStatus(value: string): void {
    this.selectedStatus.set(value as DepartmentStatus | '');
  }

  clearFilters(): void {
    this.searchTerm.set('');
    this.selectedStatus.set('');
  }
}
