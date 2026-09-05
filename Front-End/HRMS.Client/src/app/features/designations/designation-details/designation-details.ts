import {
  ChangeDetectionStrategy,
  Component,
  signal
} from '@angular/core';
import { DatePipe } from '@angular/common';
import {
  ActivatedRoute,
  Router,
  RouterLink
} from '@angular/router';

import { DESIGNATION_MOCK_DATA } from '../data/designation.mock';
import { DesignationModel } from '../models/designation.model';

@Component({
  imports: [DatePipe, RouterLink],
  selector: 'app-designation-details',
  styleUrl: './designation-details.scss',
  templateUrl: './designation-details.html',
})
export class DesignationDetails {
   readonly designation = signal<DesignationModel | null>(null);

  readonly designationId = signal<number | null>(null);

  constructor(
    private readonly route: ActivatedRoute,
    private readonly router: Router
  ) {
    this.loadDesignation();
  }

  private loadDesignation(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const id = Number(idParam);

    if (!idParam || Number.isNaN(id)) {
      this.designation.set(null);
      return;
    }

    this.designationId.set(id);

    const foundDesignation = DESIGNATION_MOCK_DATA.find(
      (designation) => designation.id === id
    );

    this.designation.set(foundDesignation ?? null);
  }

  goBack(): void {
    this.router.navigate(['/designations']);
  }
}
