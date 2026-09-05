import { Component, computed, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { OrganizationModel } from '../models/organization.model';
import { ORGANIZATION_MOCK_DATA } from '../data/organization.mock';

@Component({
  selector: 'app-organization-details',
  standalone: true,
  imports: [
    CommonModule,
    RouterLink
  ],
  templateUrl: './organization-details.html',
  styleUrl: './organization-details.scss'
})
export class OrganizationDetails {

  organization = signal<OrganizationModel | null>(null);

  organizationId = signal<number | null>(null);

  initials = computed(() => {
    const organization = this.organization();

    if (!organization) {
      return '';
    }

    return organization.name
      .split(' ')
      .filter(Boolean)
      .slice(0, 2)
      .map(word => word.charAt(0))
      .join('')
      .toUpperCase();
  });

  constructor(
    private route: ActivatedRoute
  ) {
    const id = Number(
      this.route.snapshot.paramMap.get('id')
    );

    if (!Number.isNaN(id)) {
      this.organizationId.set(id);
      this.loadOrganization(id);
    }
  }

  private loadOrganization(id: number): void {
    const organization = ORGANIZATION_MOCK_DATA.find(
      item => item.id === id
    );

    this.organization.set(organization ?? null);
  }
}