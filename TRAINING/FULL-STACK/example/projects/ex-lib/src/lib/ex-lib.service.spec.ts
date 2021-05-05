import { TestBed } from '@angular/core/testing';

import { ExLibService } from './ex-lib.service';

describe('ExLibService', () => {
  beforeEach(() => TestBed.configureTestingModule({}));

  it('should be created', () => {
    const service: ExLibService = TestBed.get(ExLibService);
    expect(service).toBeTruthy();
  });
});
