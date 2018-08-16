import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaderResponse, HttpHeaders, HttpErrorResponse } from '@angular/common/http';
import { Response } from "@angular/http";
import { Observable, throwError } from 'rxjs';
import { User } from './user.model';
import { catchError } from 'rxjs/operators'

@Injectable()
export class UserService {
  readonly rootUrl = 'http://localhost:49859';
  constructor(private http: HttpClient) { }

  registerUser(user: User): Observable<any> {
    const body: User = {
      Email: user.Email,
      UserName: user.UserName,
      Password: user.Password,
      FirstName: user.FirstName,
      LastName: user.LastName
    }
    return this.http.post('/api/Account/Register', body)
      .pipe(
        catchError((err) => this.handleError(err))
      );
  }

  userAuthentication(userName, password) {
    var data = "UserName=" + userName + "&Password=" + password + "&grant_type=password";
    var reqHeader = new HttpHeaders({ 'Content-Type': 'application/x-www-form-urlencoded' });
    return this.http.post('/token', data, { headers: reqHeader });
  }

  private handleError(error: HttpErrorResponse) {
    if (error.error instanceof ErrorEvent) {
      console.error('An error occurred:', error.error.message);
    }
    else {
      console.error('Backend returned code ${error.status}, body was: ${error.error}');
    }
    return throwError(
      'Something bad happened; please try again later.');
  }
}