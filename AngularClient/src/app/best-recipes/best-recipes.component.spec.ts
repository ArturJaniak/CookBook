import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BestRecipesComponent } from './best-recipes.component';

describe('BestRecipesComponent', () => {
  let component: BestRecipesComponent;
  let fixture: ComponentFixture<BestRecipesComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BestRecipesComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BestRecipesComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
