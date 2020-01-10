import { Injectable } from '@angular/core';
import { User } from './user';
import { AuthenticationService } from './authentication.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  currentUser: User;

  constructor(private authenticationService: AuthenticationService, private http: HttpClient) {
    authenticationService.currentUser.subscribe(x => this.currentUser = x);
   }

   getLatest() {
     return this.http.get(`${environment.apiUrl}/Statistic/GetLatestById?id=${this.currentUser.VortexId}`);
   }

   getVortexIdByUserId() {
     return this.http.get(`${environment.apiUrl}/User/GetVortexId?userId=${this.currentUser.UserId}`);
   }
}
