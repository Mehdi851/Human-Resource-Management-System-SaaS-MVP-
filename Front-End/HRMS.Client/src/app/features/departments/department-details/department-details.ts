import {
  ChangeDetectionStrategy,
  Component,
  computed
} from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';

import {
  DepartmentModel
} from '../models/department.model';

import {
  DEPARTMENT_RECORDS
} from '../data/department.mock';

@Component({
  imports: [RouterLink],
  selector: 'app-department-details',
  styleUrl: './department-details.scss',
  templateUrl: './department-details.html',
})
export class DepartmentDetails {
   readonly departmentRecords = DEPARTMENT_RECORDS;

  readonly department = computed<DepartmentModel | undefined>(() => {
    const id = Number(
      this.route.snapshot.paramMap.get('id')
    );

    return this.departmentRecords.find(
      department => department.id === id
    );
  });

  constructor(
    private readonly route: ActivatedRoute
  ) {}
}
