import {
  ChangeDetectionStrategy,
  Component,
  computed,
  signal
} from '@angular/core';
import { RouterLink } from '@angular/router';

import { DESIGNATION_MOCK_DATA } from './data/designation.mock';
import {
  DesignationModel,
  DesignationStatus
} from './models/designation.model';

@Component({
  imports: [RouterLink],
  standalone: true,
  selector: 'app-designations',
  styleUrl: './designations.scss',
  templateUrl: './designations.html',
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class Designations {
   readonly designations = signal<DesignationModel[]>(DESIGNATION_MOCK_DATA);

  readonly searchTerm = signal('');

  readonly selectedStatus = signal<'All' | DesignationStatus>('All');

  readonly filteredDesignations = computed(() => {
    const search = this.searchTerm().trim().toLowerCase();
    const status = this.selectedStatus();

    return this.designations().filter((designation) => {
      const matchesSearch =
        !search ||
        designation.name.toLowerCase().includes(search) ||
        designation.description.toLowerCase().includes(search) ||
        designation.department.toLowerCase().includes(search);

      const matchesStatus =
        status === 'All' ||
        designation.status === status;

      return matchesSearch && matchesStatus;
    });
  });

  readonly totalDesignations = computed(
    () => this.designations().length
  );

  readonly activeDesignations = computed(
    () =>
      this.designations().filter(
        (designation) => designation.status === 'Active'
      ).length
  );

  readonly inactiveDesignations = computed(
    () =>
      this.designations().filter(
        (designation) => designation.status === 'Inactive'
      ).length
  );

  readonly totalEmployees = computed(
    () =>
      this.designations().reduce(
        (total, designation) => total + designation.employeeCount,
        0
      )
  );

  onSearchChange(event: Event): void {
    const input = event.target as HTMLInputElement;
    this.searchTerm.set(input.value);
  }

  onStatusChange(event: Event): void {
    const select = event.target as HTMLSelectElement;
    const value = select.value;

    if (value === 'Active' || value === 'Inactive') {
      this.selectedStatus.set(value);
      return;
    }

    this.selectedStatus.set('All');
  }
}
