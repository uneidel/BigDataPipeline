import { Component, Pipe, PipeTransform } from '@angular/core';
import { MovieService } from '../services/movie.service';
import { InteractionService } from '../services/Interaction.service';
import { FingerPrintService } from '../services/fingerprint.service';
import { Http } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/toPromise';


@Component({
    selector: 'appscroller',
    templateUrl: './Videoscroller.html',
    providers: [MovieService, InteractionService, FingerPrintService]
   
})

export class Scroller {
    movieIterator;
    hash;
    constructor(private http: Http, private interaction: InteractionService, private fingerprintservice: FingerPrintService) {
        const self = this;
        this.movieIterator = {
            next: function (fromIndex, length) {
                return {
                    value: self.http.get('/api/movies/${fromIndex}').toPromise().then(response => response.json()),
                    done: false
                };
            }
        };
        this.fingerprintservice.get().then(res => this.hash = res);

    }
    DoAction(item) {
        this.interaction.UpdateUserAction(this.hash, item);
        

    }
   /*
    constructor(private movieservice: MovieService) {
        const self = this;

        this.movieIterator = {
            next: function (fromIndex, length) {
                return {
                    value: self.movieservice.getMovies(5).subscribe.t(res => {
                        console.log(res);
                        res
                    }
                        , err => { console.error(err); }
                    ),
                    done: false
                };
            }
        };
    }
    /*
    constructor(private movieservice: MovieService) {
        

      this.movieservice.getMovies(5).subscribe(
            function (response)
            {
                console.log("success");
                console.log(response);
                this.result = response;
            },
            function (error) { console.log("Error happened") },
            function () { console.log("the subscription is completed") }
        );
    
        
        const self = this;

        this.movieIterator = {
            next: function (fromIndex, length) {
                let value = [];
                self.movieservice.getMovies(fromIndex).subscribe(
                    function (response) {
                        this.value = response;
                        console.log("success");
                    },
                    function (error) { console.log("Error happened") },
                    function () { console.log("the subscription is completed") }
                );

                return {
                    value: value,
                    done: false
                };
            }
        };

    }

   
     */
}