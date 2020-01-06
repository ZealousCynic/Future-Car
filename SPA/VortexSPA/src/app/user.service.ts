import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import {Observable, of} from 'rxjs'
import {map } from 'rxjs/operators';
import {IUser} from '../IUser';


@Injectable({
  providedIn: 'root'
})
export class UserService {

configUrl = 'http://localhost:3797/api/user/'

  constructor(private http: HttpClient) { }

  getUser(id:string):Observable<IUser> {
    return this.http
    .get<IUser>(this.configUrl + "getuser/" + id);
  }
}
