import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'boolToText'
})
export class BoolToTextPipe implements PipeTransform {

  transform(value: any, ...args: any[]): any {
    let toReturn = 'Cant pipe';
    console.log(args);

    if (args[0] == "power") {
      if (!value) {
        toReturn = 'Slukket';
      } else {
        toReturn = 'Tændt'
      }
    } 

    if (args[0] == "mode") {
      if (!value) {
        toReturn = 'Land Mode'
      } else {
        toReturn = 'Vand Mode'
      }
    }

    return toReturn;
  }

}
