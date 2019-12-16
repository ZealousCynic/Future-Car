import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.scss']
})
export class LogInComponent implements OnInit {
  loginData: FormGroup

  constructor() { }

  ngOnInit() {
    this.loginData = new FormGroup({
      username: new FormControl(),
      password: new FormControl()
    })
  }

  onLoginSubmit() {
    alert('Entered username is: ' + this.loginData.get('username').value + '. And password is: ' + this.loginData.get('password').value);
  }

}
