import { Component, OnInit } from '@angular/core';
import { UserService } from '../shared/user.service';
import { HttpErrorResponse } from '@angular/common/http';
import { Router } from '@angular/router';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.css']
})
export class SignInComponent implements OnInit {
  isLoginError : boolean = false;
  loading = false;
    
  constructor(private userService : UserService,private router : Router) { }

  ngOnInit() {
  }

  OnSubmit(userName,password){
    this.loading=true;
     this.userService.userAuthentication(userName,password).subscribe(data=>{
      localStorage.setItem('userToken',data.access_token);
      this.router.navigate(['/home']);
    },
    (err : HttpErrorResponse)=>{
      this.isLoginError = true;
      this.loading = false;
    });
  }

}

