import { Observable } from 'rxjs';

import { Customer } from '../customer.payload';

export interface ICustomerStoreService {
    getCustomerInfo(customerId: string): void;
    updateSignalR(group: string, name: string, message: Customer): void;
}
