import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AttendanceStatusBadge } from './attendance-status-badge';

describe('AttendanceStatusBadge', () => {
  let component: AttendanceStatusBadge;
  let fixture: ComponentFixture<AttendanceStatusBadge>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AttendanceStatusBadge],
    }).compileComponents();

    fixture = TestBed.createComponent(AttendanceStatusBadge);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
