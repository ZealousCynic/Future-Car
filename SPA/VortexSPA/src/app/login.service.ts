import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http'
import {Observable, of} from 'rxjs'
import {map } from 'rxjs/operators';
import {IUser} from '../IUser';

@Injectable({
  providedIn: 'root'
})
export class LoginService {

  constructor(private http:HttpClient) { }

  configUrl = 'http://localhost:3797/api/login/'

  login(user:IUser) {

    return this.http.post(this.configUrl + 'login', user);
  }
}
