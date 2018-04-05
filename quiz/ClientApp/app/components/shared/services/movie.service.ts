import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import 'rxjs/add/operator/map';

@Injectable()
export class MovieService {
    constructor(private http: Http) { }

    getMovies(id) {
        return this
            .http.get("http://localhost:5000/api/movies/" + id)
            .map((res: Response) => res.json());
    }

}