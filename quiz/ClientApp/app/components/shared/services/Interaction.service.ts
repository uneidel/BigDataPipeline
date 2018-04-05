import { Injectable } from '@angular/core';
import { Http,Headers, Response } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class InteractionService {
    constructor(private http: Http) { }

    UpdateUserAction(uid, aid) { // uid - UseriD; aid - currently movieId
        var body = "userid=" + uid + "?action=" + aid;
        var headers = new Headers();
       
        headers.append('Content-Type', 'application/json');
        return this
            .http
            .post('/api/interaction',
            JSON.stringify(body), {
                headers: headers
            })
            .subscribe(data => {
              
            }, error => {
                console.log(JSON.stringify(error.json()));
            });
    }

}