import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Rx';
import 'rxjs/add/operator/map';

declare var Fingerprint2: any;

@Injectable()
export class FingerPrintService {

    private fpPromise: Promise<string>;

    constructor() { }

    public get() {
       
        var p = new Promise<string>((resolve, reject) => {
            var fp = new Fingerprint2();
            fp.get(function (result, components) {
                resolve(result);
            })
        });
        return p;
    }
}