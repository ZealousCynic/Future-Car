import { Component, OnInit } from '@angular/core';
import { FormGroup, FormControl } from '@angular/forms';
import { UserService } from  '../../user.service'
import {IUser } from '../../../IUser'
import { LoginService } from 'src/app/login.service';

@Component({
  selector: 'app-log-in',
  templateUrl: './log-in.component.html',
  styleUrls: ['./log-in.component.scss']
})
export class LogInComponent implements OnInit {
  loginData: FormGroup;
  user:IUser;

  constructor(private userservice:UserService, private loginservice:LoginService) {
    //this.getUser();
   }

  ngOnInit() {
    this.loginData = new FormGroup({
      username: new FormControl(),
      password: new FormControl()
    })
  }

  getUser():void {
    this.userservice.getUser("5").subscribe(_s_user => this.user = _s_user);
  }

  onLoginSubmit() {
    
    let toValidate:IUser = {
      UserId: 0,
      Username:  this.loginData.get('username').value,
      Password: this.loginData.get('password').value
    };

    alert(this.loginservice.login(toValidate));
    alert('Entered username is: ' + this.loginData.get('username').value + '. And password is: ' + this.loginData.get('password').value);
  }

}
