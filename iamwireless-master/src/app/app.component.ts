import {Component, OnInit} from '@angular/core';
import { AuthenticationService } from './authentication.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent implements OnInit {
  constructor(public auth: AuthenticationService) {}
  ngOnInit() {
  if (this.auth.isLoggedIn()) { document.getElementById('Login').innerHTML = this.auth.getUserDetails().email;
  document.getElementById('Logout').innerHTML = 'Logout'; }
  }
}
