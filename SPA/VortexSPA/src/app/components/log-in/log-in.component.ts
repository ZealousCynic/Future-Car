import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Router, ActivatedRoute } from '@angular/router';
import { AuthenticationService } from 'src/app/authentication.service';
import { first } from 'rxjs/operators';
import { StatisticsService } from 'src/app/statistics.service';

@Component({
	selector: 'app-log-in',
	templateUrl: './log-in.component.html',
	styleUrls: ['./log-in.component.scss']
})
export class LogInComponent implements OnInit {
	loginForm: FormGroup;
	loading = false;
	submitted = false;
	returnUrl: string;
	error = '';
	execPath = "C:\\Program Files\\Mozilla Firefox\\firefox.exe";

	constructor(private formBuilder: FormBuilder, 
		private route: ActivatedRoute, 
		private router: Router, 
		private authenticationService: AuthenticationService,
		private statisticsService: StatisticsService) {
		if (authenticationService.currentUserValue) {
			router.navigate(['/']);
		}
	}

	ngOnInit() {
		this.loginForm = this.formBuilder.group({
			username: ['', Validators.required],
			password: ['', Validators.required]
		});

		this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
	}

	get f() { return this.loginForm.controls; }

	onSubmit() {
		this.submitted = true;

		if (this.loginForm.invalid) {
			return;
		}

		this.loading = true;
		this.authenticationService.login(this.f.username.value, this.f.password.value)
			.pipe(first())
			.subscribe(
				() => {
					this.router.navigate([this.returnUrl]);
				},
				error => {
					this.error = error;
					this.loading = false;
				});
	}
}
