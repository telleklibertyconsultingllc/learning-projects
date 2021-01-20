import { Injectable } from '@angular/core';

import { Store } from '@ngrx/store';
import { Observable } from 'rxjs';

import { Customer } from '../customer.payload';
import { ICustomerStoreService } from './customer.store.service.interface';

@Injectable({
    providedIn: 'root'
})
export class CustomerStoreService implements ICustomerStoreService {
    constructor(
        private storeSvc: Store<any>
    ) {

    }

    getCustomerInfo(customerId: string): void {

    }

    updateSignalR(group: string, name: string, message: Customer) {

    }
}
