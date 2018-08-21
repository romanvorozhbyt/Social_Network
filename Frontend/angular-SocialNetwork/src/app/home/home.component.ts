import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import {ReqResService} from '../shared/req-res.service'
import {UserHome} from '../shared/user.home'
import { NgForm } from '../../../node_modules/@angular/forms';



@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {
  public currentUser : UserHome;
  id :string;
  errorMessage:string = '';
  constructor(private userService:ReqResService, private router : Router) { }

  ngOnInit() {
    this.getMyProfile();
  }

  getMyProfile(){

    this.userService.getProfile().subscribe(
      data=> this.currentUser = data,
      error => this.errorMessage = error.message )
    
  }
  OnSubmit(userForm:NgForm){
    this.currentUser.Id = this.id;
    this.currentUser.FirstName = userForm.value.FirstName;
    this.currentUser.LastName = userForm.value.LastName;
    this.currentUser.DateOfBirth = userForm.value.DateOfBirth;
    this.currentUser.Country = userForm.value.Country;
    this.currentUser.City = userForm.value.City;
    this.currentUser.Address = userForm.value.Address;
    this.currentUser.WebSite = userForm.value.WebSite;

    this.userService.updateProfile(this.currentUser).subscribe((data : any)=>{
     
     this.router.navigate(['/home']);
   });
   
 }
logout()
{
  localStorage.removeItem("userToken");
  this.router.navigate(['/login']);
}

}
