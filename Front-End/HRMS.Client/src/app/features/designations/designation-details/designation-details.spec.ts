import { ComponentFixture, TestBed } from '@angular/core/testing';
import { DesignationDetails } from './designation-details';

describe('DesignationDetails', () => {
  let component: DesignationDetails;
  let fixture: ComponentFixture<DesignationDetails>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [DesignationDetails],
    }).compileComponents();

    fixture = TestBed.createComponent(DesignationDetails);
    component = fixture.componentInstance;
    await fixture.whenStable();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
