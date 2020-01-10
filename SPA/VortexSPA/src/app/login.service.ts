import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders, HttpErrorResponse } from '@angular/common/http'
import { Observable, of, throwError, from } from 'rxjs'
import { map } from 'rxjs/operators';
import { IUser } from '../IUser';

@Injectable({
	providedIn: 'root'
})
export class LoginService {

	url: string;
	user: IUser;

	constructor(private http: HttpClient) {

		// API URL
		this.url = 'http://localhost:3797/api/login/';
	}

	login(username: string, password: string) {
		this.user = {
			UserId: 0,
			Username: username,
			Password: password
		};

		console.log(this.user);

		return this.http.post(this.url + 'login', this.user).subscribe({
			next: data => console.log(data),
			error: error => console.error('There was an error!', error)
		})
	}
}
