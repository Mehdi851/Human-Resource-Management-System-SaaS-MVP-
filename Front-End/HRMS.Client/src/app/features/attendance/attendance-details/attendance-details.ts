import { Component, computed, inject } from '@angular/core';
import { ActivatedRoute, RouterLink } from '@angular/router';

import { AttendanceModel } from '../models/attendance.model';
import { ATTENDANCE_RECORDS } from '../data/attendance.mock';
@Component({
  imports: [RouterLink],
  selector: 'app-attendance-details',
  styleUrl: './attendance-details.scss',
  templateUrl: './attendance-details.html',
})
export class AttendanceDetails {
  private readonly route = inject(ActivatedRoute);

  readonly attendanceRecord = computed<AttendanceModel | undefined>(() => {
    const id = Number(this.route.snapshot.paramMap.get('id'));

    return ATTENDANCE_RECORDS.find(record => record.id === id);
  });
}
