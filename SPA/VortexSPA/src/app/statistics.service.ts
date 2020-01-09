import { Injectable } from '@angular/core';
import { User } from './user';
import { AuthenticationService } from './authentication.service';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class StatisticsService {
  currentUser: User;

  constructor(private authenticationService: AuthenticationService, private http: HttpClient) {
    authenticationService.currentUser.subscribe(x => this.currentUser = x);
   }

   getLatest() {
     this.http.get<any>(`${environment.apiUrl}/?Statistic/GetLatestByIdid=${this.currentUser.VortexId}`)
   }
}
