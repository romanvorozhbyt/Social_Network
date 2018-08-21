import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import {FormsModule} from '@angular/forms';
import {BrowserAnimationsModule} from '@angular/platform-browser/animations';
import {RouterModule} from '@angular/router';

import { AppComponent } from './app.component';
import { SignUpComponent } from './sign-up/sign-up.component';
import { HttpClientModule } from '@angular/common/http';
import { UserService } from './shared/user.service';
import {ReqResService} from './shared/req-res.service'
import {ToastrModule} from 'ngx-toastr';
import { UserComponent } from './user/user.component';
import {HomeComponent} from './home/home.component';
import {SignInComponent} from './sign-in/sign-in.component';
import { appRoutes } from './routes';

@NgModule({
  declarations: [
    AppComponent,
    SignUpComponent,
    UserComponent,
    HomeComponent,
    SignInComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    BrowserAnimationsModule,
    RouterModule.forRoot(appRoutes)
    

  ],
  providers: [UserService, ReqResService], 
  bootstrap: [AppComponent]
})
export class AppModule { }
