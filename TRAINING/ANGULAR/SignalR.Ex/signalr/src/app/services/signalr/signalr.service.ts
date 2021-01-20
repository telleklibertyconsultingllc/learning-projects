import { Injectable } from '@angular/core';

import * as signalR from '@microsoft/signalr';
import { BehaviorSubject } from 'rxjs';

import * as fromPayload from '../../store/customer/customer.payload';

@Injectable({
    providedIn: 'root'
})
export class SignalRService {
    connection: signalR.HubConnection;
    hubHelloMessage: BehaviorSubject<fromPayload.Message>;

    constructor() {
        this.hubHelloMessage = new BehaviorSubject<fromPayload.Message>(null);
    }

    private setSignalRClientMethods(): void {
        this.connection.on('UpdateExample', (message: fromPayload.Message) => {
            this.hubHelloMessage.next(message);
        });
    }

    public setupConnection(): Promise<void> {
        return new Promise((resolve, reject) => {
            this.connection = new signalR.HubConnectionBuilder()
                .withUrl('https://localhost:44307/example') ///  signalR.HttpTransportType.WebSocket
                .build();
            this.setSignalRClientMethods();
            this.connection.start()
                .then(() => {
                    console.log('SignalR Connection success!');
                    resolve();
                })
                .catch((error) => {
                    console.log(`SignalR connection error: ${error}`);
                    reject();
                });
        });
    }
}