import { Component, OnInit } from '@angular/core';
import {AuthenticationService} from '../authentication.service';
import {Router} from '@angular/router';

@Component({
  selector: 'app-logout',
  templateUrl: './logout.component.html',
  styleUrls: ['./logout.component.css']
})
export class LogoutComponent implements OnInit {

  constructor(private auth: AuthenticationService, private router: Router) { }

  ngOnInit() {
    this.auth.logout();
    if (this.auth.isLoggedIn()) { document.getElementById('Login').innerHTML = this.auth.getUserDetails().email;
      document.getElementById('Logout').innerHTML = 'Logout'; }
    this.router.navigateByUrl('/home');
  }

}
