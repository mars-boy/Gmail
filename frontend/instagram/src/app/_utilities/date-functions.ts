import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class DateFunctions {

    getUtcTicks(){
        var date = new Date();
        var utcDate = Date.UTC(
            date.getFullYear(), date.getMonth(), date.getDay(),
             date.getHours(), date.getMinutes(), 
             date.getSeconds(), date.getMilliseconds()
        );
        return utcDate;
    }
}
