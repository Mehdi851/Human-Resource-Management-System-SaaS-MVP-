import { ComponentFixture, TestBed } from '@angular/core/testing';
import { LeaveTypeBadge } from './leave-type-badge';

describe('LeaveTypeBadge', () => {
  let component: LeaveTypeBadge;
  let fixture: ComponentFixture<LeaveTypeBadge>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LeaveTypeBadge],
    }).compileComponents();

    fixture = TestBed.createComponent(LeaveTypeBadge);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
