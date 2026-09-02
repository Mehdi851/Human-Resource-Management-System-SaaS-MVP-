import {ChangeDetectionStrategy, Component, signal,computed } from '@angular/core';
import { RouterLink } from '@angular/router';
import { DatePipe } from '@angular/common';
import { EmployeeStatusBadge } from './components/employee-status-badge/employee-status-badge';
import { MOCK_EMPLOYEES } from '../employees/data/employee.mock';
import { Employee, EmploymentStatus } from '../employees/models/employee.model';
import { FormsModule } from '@angular/forms';
@Component({
  imports: [RouterLink, EmployeeStatusBadge,DatePipe, FormsModule],
  standalone: true,
  selector: 'app-employees',
  styleUrl: './employees.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './employees.html',
})
export class Employees { // --------------------------------------------------
  // Configuration
  // --------------------------------------------------

  readonly pageSize = 5;

  // --------------------------------------------------
  // Source Data
  // --------------------------------------------------

  readonly employees = signal<readonly Employee[]>(
    MOCK_EMPLOYEES,
  );

  // --------------------------------------------------
  // Search & Filters
  // --------------------------------------------------

  readonly searchTerm = signal('');

  readonly selectedDepartment = signal('All Departments');

  readonly selectedDesignation = signal('All Designations');

  readonly selectedStatus = signal<
    'All Statuses' | EmploymentStatus
  >('All Statuses');

  // --------------------------------------------------
  // Pagination
  // --------------------------------------------------

  readonly currentPage = signal(1);

  // --------------------------------------------------
  // Filter Options
  // --------------------------------------------------

  readonly departments = computed(() => [
    'All Departments',
    ...new Set(
      this.employees().map(
        employee => employee.department,
      ),
    ),
  ]);

  readonly designations = computed(() => [
    'All Designations',
    ...new Set(
      this.employees().map(
        employee => employee.designation,
      ),
    ),
  ]);

  readonly statuses: readonly (
    | 'All Statuses'
    | EmploymentStatus
  )[] = [
    'All Statuses',
    'Active',
    'Inactive',
    'On Leave',
  ];

  // --------------------------------------------------
  // Filtered Employees
  // --------------------------------------------------

  readonly filteredEmployees = computed(() => {
    const search = this.searchTerm()
      .trim()
      .toLowerCase();

    const department =
      this.selectedDepartment();

    const designation =
      this.selectedDesignation();

    const status =
      this.selectedStatus();

    return this.employees().filter(employee => {
      const matchesSearch =
        !search ||
        `${employee.firstName} ${employee.lastName}`
          .toLowerCase()
          .includes(search) ||
        employee.employeeCode
          .toLowerCase()
          .includes(search) ||
        employee.email
          .toLowerCase()
          .includes(search);

      const matchesDepartment =
        department === 'All Departments' ||
        employee.department === department;

      const matchesDesignation =
        designation === 'All Designations' ||
        employee.designation === designation;

      const matchesStatus =
        status === 'All Statuses' ||
        employee.employmentStatus === status;

      return (
        matchesSearch &&
        matchesDepartment &&
        matchesDesignation &&
        matchesStatus
      );
    });
  });

  // --------------------------------------------------
  // Pagination
  // --------------------------------------------------

  readonly totalPages = computed(() => {
    return Math.max(
      1,
      Math.ceil(
        this.filteredEmployees().length /
          this.pageSize,
      ),
    );
  });

  readonly paginatedEmployees = computed(() => {
    const start =
      (this.currentPage() - 1) *
      this.pageSize;

    const end = start + this.pageSize;

    return this.filteredEmployees().slice(
      start,
      end,
    );
  });

  readonly pageNumbers = computed(() => {
    return Array.from(
      { length: this.totalPages() },
      (_, index) => index + 1,
    );
  });

  readonly showingFrom = computed(() => {
    const total =
      this.filteredEmployees().length;

    if (total === 0) {
      return 0;
    }

    return (
      (this.currentPage() - 1) *
        this.pageSize +
      1
    );
  });

  readonly showingTo = computed(() => {
    return Math.min(
      this.currentPage() * this.pageSize,
      this.filteredEmployees().length,
    );
  });

  // --------------------------------------------------
  // Search
  // --------------------------------------------------

  onSearch(value: string): void {
    this.searchTerm.set(value);
    this.currentPage.set(1);
  }

  // --------------------------------------------------
  // Filters
  // --------------------------------------------------

  onDepartmentChange(
    value: string,
  ): void {
    this.selectedDepartment.set(value);
    this.currentPage.set(1);
  }

  onDesignationChange(
    value: string,
  ): void {
    this.selectedDesignation.set(value);
    this.currentPage.set(1);
  }

  onStatusChange(
    value:
      | 'All Statuses'
      | EmploymentStatus,
  ): void {
    this.selectedStatus.set(value);
    this.currentPage.set(1);
  }

  clearFilters(): void {
    this.searchTerm.set('');
    this.selectedDepartment.set(
      'All Departments',
    );
    this.selectedDesignation.set(
      'All Designations',
    );
    this.selectedStatus.set(
      'All Statuses',
    );
    this.currentPage.set(1);
  }

  // --------------------------------------------------
  // Pagination Actions
  // --------------------------------------------------

  goToPage(page: number): void {
    if (
      page < 1 ||
      page > this.totalPages()
    ) {
      return;
    }

    this.currentPage.set(page);
  }

  previousPage(): void {
    this.goToPage(
      this.currentPage() - 1,
    );
  }

  nextPage(): void {
    this.goToPage(
      this.currentPage() + 1,
    );
  }

  // --------------------------------------------------
  // Empty / Filter State
  // --------------------------------------------------

  readonly hasActiveFilters = computed(() => {
    return (
      this.searchTerm().trim().length > 0 ||
      this.selectedDepartment() !==
        'All Departments' ||
      this.selectedDesignation() !==
        'All Designations' ||
      this.selectedStatus() !==
        'All Statuses'
    );
  });

// --------------------------------------------------
  // Delete Employee Modal
  // --------------------------------------------------

  readonly showDeleteModal = signal(false);

  readonly employeePendingDelete =
    signal<Employee | null>(null);

  openDeleteConfirmation(
    employee: Employee,
  ): void {
    this.employeePendingDelete.set(employee);
    this.showDeleteModal.set(true);
  }

  closeDeleteConfirmation(): void {
    this.employeePendingDelete.set(null);
    this.showDeleteModal.set(false);
  }

  confirmDelete(): void {
    const employee =
      this.employeePendingDelete();

    if (!employee) {
      return;
    }

    /*
    * Phase 2:
    * UI-only implementation.
    *
    * Actual delete API will be implemented
    * during Phase 3.
    */

    console.log(
      'Delete employee:',
      employee.id,
    );

    this.closeDeleteConfirmation();
  }
}
