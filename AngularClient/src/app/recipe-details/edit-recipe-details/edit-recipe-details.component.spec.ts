import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { EditRecipeDetailsComponent } from './edit-recipe-details.component';

describe('EditRecipeDetailsComponent', () => {
  let component: EditRecipeDetailsComponent;
  let fixture: ComponentFixture<EditRecipeDetailsComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ EditRecipeDetailsComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(EditRecipeDetailsComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
