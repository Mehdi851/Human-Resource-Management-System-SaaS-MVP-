import { Component, computed, inject, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators
} from '@angular/forms';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';

import { OrganizationModel } from '../models/organization.model';
import { ORGANIZATION_MOCK_DATA } from '../data/organization.mock';

@Component({
  selector: 'app-organization-form',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    RouterLink
  ],
  templateUrl: './organization-form.html',
  styleUrl: './organization-form.scss'
})
export class OrganizationForm {

  private readonly fb = inject(FormBuilder);

  isEditMode = signal(false);
  organizationId = signal<number | null>(null);

  pageTitle = computed(() =>
    this.isEditMode()
      ? 'Edit Organization'
      : 'Add Organization'
  );

  pageDescription = computed(() =>
    this.isEditMode()
      ? 'Update organization information and configuration.'
      : 'Create a new organization in your HRMS system.'
  );

  organizationForm = this.fb.nonNullable.group({
    name: ['', [
      Validators.required,
      Validators.maxLength(100)
    ]],

    code: ['', [
      Validators.required,
      Validators.maxLength(20)
    ]],

    email: ['', [
      Validators.required,
      Validators.email
    ]],

    phone: ['', [
      Validators.required,
      Validators.maxLength(30)
    ]],

    website: ['', [
      Validators.maxLength(200)
    ]],

    address: ['', [
      Validators.required,
      Validators.maxLength(200)
    ]],

    city: ['', [
      Validators.required,
      Validators.maxLength(100)
    ]],

    country: ['', [
      Validators.required,
      Validators.maxLength(100)
    ]],

    status: ['Active' as 'Active' | 'Inactive', [
      Validators.required
    ]]
  });

  constructor(
    private route: ActivatedRoute,
    private router: Router
  ) {
    const id = this.route.snapshot.paramMap.get('id');

    if (id) {
      const organizationId = Number(id);

      if (!Number.isNaN(organizationId)) {
        this.isEditMode.set(true);
        this.organizationId.set(organizationId);

        this.loadOrganization(organizationId);
      }
    }
  }

  private loadOrganization(id: number): void {
    const organization = ORGANIZATION_MOCK_DATA.find(
      item => item.id === id
    );

    if (!organization) {
      return;
    }

    this.organizationForm.patchValue({
      name: organization.name,
      code: organization.code,
      email: organization.email,
      phone: organization.phone,
      website: organization.website ?? '',
      address: organization.address,
      city: organization.city,
      country: organization.country,
      status: organization.status
    });
  }

  isInvalid(controlName: string): boolean {
    const control = this.organizationForm.get(controlName);

    return !!(
      control &&
      control.invalid &&
      (control.dirty || control.touched)
    );
  }

  save(): void {
    if (this.organizationForm.invalid) {
      this.organizationForm.markAllAsTouched();
      return;
    }

    const formValue = this.organizationForm.getRawValue();

    console.log(
      this.isEditMode()
        ? 'Updating organization:'
        : 'Creating organization:',
      formValue
    );

    this.router.navigate(['/organizations']);
  }

  cancel(): void {
    this.router.navigate(['/organizations']);
  }
}