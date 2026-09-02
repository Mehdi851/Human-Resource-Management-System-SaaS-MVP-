import { ComponentFixture, TestBed } from '@angular/core/testing';
import { EmployeeEmptyState } from './employee-empty-state';

describe('EmployeeEmptyState', () => {
  let component: EmployeeEmptyState;
  let fixture: ComponentFixture<EmployeeEmptyState>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [EmployeeEmptyState],
    }).compileComponents();

    fixture = TestBed.createComponent(EmployeeEmptyState);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
