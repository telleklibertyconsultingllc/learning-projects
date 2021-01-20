import { Message } from '@angular/compiler/src/i18n/i18n_ast';
import {
    Component,
    OnInit,
    OnDestroy
} from '@angular/core';

import { SignalRService } from '../services/signalr/signalr.service';
import* as fromPayload from '../store/customer/customer.payload';

@Component({
    selector: 'app-customer',
    templateUrl: './customers.component.html',
    styleUrls: [
        './customers.component.scss'
    ]
})
export class CustomerComponent implements OnInit, OnDestroy {
    helloMessage: fromPayload.Message;

    constructor(
        public signalRSvc: SignalRService
    ) {}

    ngOnInit() {
        this.signalRSvc.hubHelloMessage.subscribe((message: fromPayload.Message) => {
            this.helloMessage = message;
        });
        this.signalRSvc.connection.invoke('UpdateExample')
        .catch((error) => {
            console.log('error', error);
            alert('SignalR Demo Error');
        });
    }

    ngOnDestroy() {

    }
}
