import { ComponentFixture, TestBed } from '@angular/core/testing';
import { WorkSettings } from './work-settings';

describe('WorkSettings', () => {
  let component: WorkSettings;
  let fixture: ComponentFixture<WorkSettings>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [WorkSettings],
    }).compileComponents();

    fixture = TestBed.createComponent(WorkSettings);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
