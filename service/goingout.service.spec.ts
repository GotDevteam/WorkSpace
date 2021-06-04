import { TestBed } from '@angular/core/testing';

import { GoingOutService } from './goingout.service';

describe('GoingoutService', () => {
  let service: GoingOutService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(GoingOutService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
