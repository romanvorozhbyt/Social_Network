import { Injectable } from '@angular/core';
import { HttpClient,HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import {UserHome} from './user.home';


@Injectable()
export class ReqResService {
  readonly rootUrl = 'http://localhost:49859';
  constructor(private http: HttpClient) { }


  getProfile() :Observable<UserHome>{
    const url = `api/user/getMyProfile`;
    var reqHeader = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem('userToken') });
    return this.http.get<UserHome>(url,{headers:reqHeader});
}

updateProfile(user: UserHome)
{
    const url = `api/user/ `+user.Id;
    var reqHeader = new HttpHeaders({ 'Authorization': 'Bearer ' + localStorage.getItem('userToken') });
    reqHeader.append('Content-Type', 'application/json');
    return this.http.put<UserHome>(url, JSON.stringify(user), {headers:reqHeader});
}

}
 
