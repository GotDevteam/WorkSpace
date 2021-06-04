import { TestBed } from '@angular/core/testing';

import { ReceivedbackService } from './receivedback.service';

describe('ReceivedbackService', () => {
  let service: ReceivedbackService;

  beforeEach(() => {
    TestBed.configureTestingModule({});
    service = TestBed.inject(ReceivedbackService);
  });

  it('should be created', () => {
    expect(service).toBeTruthy();
  });
});
