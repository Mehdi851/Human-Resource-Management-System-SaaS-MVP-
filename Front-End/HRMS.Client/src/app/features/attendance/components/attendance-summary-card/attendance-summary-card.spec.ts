import { ComponentFixture, TestBed } from '@angular/core/testing';
import { AttendanceSummaryCard } from './attendance-summary-card';

describe('AttendanceSummaryCard', () => {
  let component: AttendanceSummaryCard;
  let fixture: ComponentFixture<AttendanceSummaryCard>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [AttendanceSummaryCard],
    }).compileComponents();

    fixture = TestBed.createComponent(AttendanceSummaryCard);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
