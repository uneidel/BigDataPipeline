import { Component, OnInit } from '@angular/core';

import { FingerPrintService } from '../services/fingerprint.service';
@Component({
    selector: 'shared-header',
    templateUrl: './header.component.html',
    providers: [FingerPrintService]
})
export class HeaderComponent implements OnInit {

    hash = "foofighter";
    
    constructor(private fingerprint: FingerPrintService) {
        this.fingerprint.get().then(result => this.hash = result);
    }
    ngOnInit() {
       
    }
}