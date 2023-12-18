import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'dateEnd'
})
export class DateEndPipe implements PipeTransform {

  transform(date: Date): boolean {
      
      const todayTimestamp = Date.now();
      const dateTimestamp = Date.parse(date.toString()) 

      return dateTimestamp < todayTimestamp;
  }
}
