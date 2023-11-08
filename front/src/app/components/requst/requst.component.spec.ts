import { ComponentFixture, TestBed } from '@angular/core/testing';

import { RequstComponent } from './requst.component';

describe('RequstComponent', () => {
  let component: RequstComponent;
  let fixture: ComponentFixture<RequstComponent>;

  beforeEach(() => {
    TestBed.configureTestingModule({
      declarations: [RequstComponent]
    });
    fixture = TestBed.createComponent(RequstComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
