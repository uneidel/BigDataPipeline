import { Pipe, PipeTransform } from '@angular/core';

@Pipe({ name: 'split' })

export class SplitPipe implements PipeTransform {
    transform(val: string, separator: any, index:any): string {
        var foo = val.split(separator);
        var res = foo[index];
        return res;
    }
}