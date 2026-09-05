import { Component, computed, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

import {
  OrganizationModel,
  OrganizationStatus
} from './models/organization.model';

import { ORGANIZATION_MOCK_DATA } from './data/organization.mock';

@Component({
  selector: 'app-organizations',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './organization.html',
  styleUrl: './organization.scss'
})
export class Organizations {

  organizations = signal<OrganizationModel[]>(
    [...ORGANIZATION_MOCK_DATA]
  );

  searchTerm = signal('');

  statusFilter = signal<
    'All' | OrganizationStatus
  >('All');

  currentPage = signal(1);

  pageSize = signal(5);

  organizationToDelete = signal<OrganizationModel | null>(null);

  filteredOrganizations = computed(() => {
    const search = this.searchTerm()
      .trim()
      .toLowerCase();

    const status = this.statusFilter();

    return this.organizations().filter(organization => {

      const matchesSearch =
        !search ||
        organization.name
          .toLowerCase()
          .includes(search) ||
        organization.code
          .toLowerCase()
          .includes(search) ||
        organization.email
          .toLowerCase()
          .includes(search) ||
        organization.city
          .toLowerCase()
          .includes(search);

      const matchesStatus =
        status === 'All' ||
        organization.status === status;

      return matchesSearch && matchesStatus;
    });
  });

  totalOrganizations = computed(
    () => this.organizations().length
  );

  activeOrganizations = computed(
    () =>
      this.organizations().filter(
        organization =>
          organization.status === 'Active'
      ).length
  );
    getInitials(name: string): string {
      return name
        .split(' ')
        .filter(Boolean)
        .slice(0, 2)
        .map(word => word.charAt(0))
        .join('')
        .toUpperCase();
    }
  inactiveOrganizations = computed(
    () =>
      this.organizations().filter(
        organization =>
          organization.status === 'Inactive'
      ).length
  );

  totalEmployees = computed(
    () =>
      this.organizations().reduce(
        (total, organization) =>
          total + organization.employeeCount,
        0
      )
  );

  totalPages = computed(() =>
    Math.max(
      1,
      Math.ceil(
        this.filteredOrganizations().length /
        this.pageSize()
      )
    )
  );

  paginatedOrganizations = computed(() => {

    const start =
      (this.currentPage() - 1) *
      this.pageSize();

    const end =
      start + this.pageSize();

    return this.filteredOrganizations()
      .slice(start, end);
  });

  paginationStart = computed(() => {

    const total =
      this.filteredOrganizations().length;

    if (total === 0) {
      return 0;
    }

    return (
      (this.currentPage() - 1) *
      this.pageSize()
    ) + 1;
  });

  paginationEnd = computed(() => {

    const total =
      this.filteredOrganizations().length;

    return Math.min(
      this.currentPage() * this.pageSize(),
      total
    );
  });

  onSearch(event: Event): void {

    const input =
      event.target as HTMLInputElement;

    this.searchTerm.set(input.value);

    this.currentPage.set(1);
  }

  onStatusChange(event: Event): void {

    const select =
      event.target as HTMLSelectElement;

    this.statusFilter.set(
      select.value as
        | 'All'
        | OrganizationStatus
    );

    this.currentPage.set(1);
  }

  onPageSizeChange(event: Event): void {

    const select =
      event.target as HTMLSelectElement;

    this.pageSize.set(
      Number(select.value)
    );

    this.currentPage.set(1);
  }

  goToPage(page: number): void {

    if (
      page < 1 ||
      page > this.totalPages()
    ) {
      return;
    }

    this.currentPage.set(page);
  }

  nextPage(): void {

    if (
      this.currentPage() <
      this.totalPages()
    ) {
      this.currentPage.update(
        page => page + 1
      );
    }
  }

  previousPage(): void {

    if (this.currentPage() > 1) {
      this.currentPage.update(
        page => page - 1
      );
    }
  }

  clearFilters(): void {

    this.searchTerm.set('');
    this.statusFilter.set('All');
    this.currentPage.set(1);
  }

  openDeleteConfirmation(
    organization: OrganizationModel
  ): void {

    this.organizationToDelete.set(
      organization
    );
  }

  closeDeleteConfirmation(): void {

    this.organizationToDelete.set(null);
  }

  deleteOrganization(): void {

    const organization =
      this.organizationToDelete();

    if (!organization) {
      return;
    }

    this.organizations.update(
      organizations =>
        organizations.filter(
          item => item.id !== organization.id
        )
    );

    this.organizationToDelete.set(null);

    if (
      this.currentPage() >
      this.totalPages()
    ) {
      this.currentPage.set(
        this.totalPages()
      );
    }
  }

  trackById(
    _index: number,
    organization: OrganizationModel
  ): number {
    return organization.id;
  }
}