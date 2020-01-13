import { Injectable } from '@angular/core';
import { User } from './user';
import { AuthenticationService } from './authentication.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';
import { Statistics } from './statistics';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  currentUser: User;

  constructor(private authenticationService: AuthenticationService, private http: HttpClient) {
    authenticationService.currentUser.subscribe(x => this.currentUser = x);
   }

   getLatest() {
     return this.http.get(`${environment.apiUrl}/Statistic/GetLatestById?id=${JSON.parse(localStorage.getItem('VortexId'))}`)
     .subscribe(statistics => {
       localStorage.setItem('Statistics', JSON.stringify(statistics));
     })
   }

   getVortexIdByUserId() {
     return this.http.get(`${environment.apiUrl}/User/GetVortexId?userId=${this.currentUser.UserId}`)
     .subscribe(vortexId => {
       localStorage.setItem('VortexId', JSON.stringify(vortexId));
     })
   }
}
