import { Component , OnInit } from '@angular/core';
import { SignalRService } from '../shared/services/data.service';
import { SignalRConnectionStatus } from '../../interfaces';

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    providers: [SignalRService]
})
export class HomeComponent implements OnInit  {
    
    connectionId: string;
    error: any;

    constructor(){}
    //constructor(private signalr: SignalRService) { }

    ngOnInit() { }

  

    listenForConnection() {
        let self = this;
    }

    updateSubscription(subscription: any) {
   
    }
    
}
