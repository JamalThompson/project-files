import { Component } from '@angular/core';
import { AuthenticationService, TokenPayload } from '../authentication.service';
import { Router } from '@angular/router';
import {
  AuthService,
  FacebookLoginProvider,
  GoogleLoginProvider
} from 'angular5-social-login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  credentials: TokenPayload = {
    email: '',
    password: ''
  };

  constructor(private auth: AuthenticationService, private router: Router, private socialAuthService: AuthService) {}

  public socialSignIn(socialPlatform: string) {
    let socialPlatformProvider;
    if (socialPlatform === 'facebook') {
      socialPlatformProvider = FacebookLoginProvider.PROVIDER_ID;
    }
    this.socialAuthService.signIn(socialPlatformProvider).then(
      (userData) => {
        console.log(socialPlatform + ' sign in data : ' , userData);
      }
    );
  }

  login() {
    console.log(this.credentials);
    this.auth.login(this.credentials).subscribe(() => {
      if (this.auth.isLoggedIn()) { document.getElementById('Login').innerHTML = this.auth.getUserDetails().email;
        document.getElementById('Logout').innerHTML = 'Logout'; }
      this.router.navigateByUrl('/');
    }, (err) => {
      console.error(err);
    });
  }
}
