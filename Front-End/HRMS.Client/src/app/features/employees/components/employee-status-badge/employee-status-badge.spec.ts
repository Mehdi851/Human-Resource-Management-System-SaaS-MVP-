import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeeStatusBadge } from './employee-status-badge';

describe('EmployeeStatusBadge', () => {
  let component: EmployeeStatusBadge;
  let fixture: ComponentFixture<EmployeeStatusBadge>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeStatusBadge],
    }).compileComponents();

    fixture = TestBed.createComponent(EmployeeStatusBadge);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
