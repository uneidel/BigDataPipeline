import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { AppComponent } from './components/app/app.component'
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/shared/header/header.component';
import { QuizComponent } from './components/shared/Quiz/Quiz.Component';
import { CoolInfiniteGridModule } from 'angular2-cool-infinite-grid';
import { SplitPipe } from './components/shared/Split.pipe';
import { TruncatePipe  } from './components/shared/truncate.pipe';
import { Scroller } from './components/shared/Videoscroller/Videoscroller';


@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        HeaderComponent,
        QuizComponent,
        SplitPipe,
        TruncatePipe,
        Scroller,
        HomeComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        BrowserModule,
        CoolInfiniteGridModule,
        FormsModule,
        RouterModule.forRoot([
            { path: '', redirectTo: 'home', pathMatch: 'full' },
            { path: 'home', component: HomeComponent },
            { path: '**', redirectTo: 'home' }
        ])
    ]
})
export class AppModule {
}

