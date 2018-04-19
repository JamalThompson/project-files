import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {AuthenticationService} from '../authentication.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  products: any;
  constructor(private auth: AuthenticationService, private http: HttpClient) { }

  ngOnInit() {
    this.http.get('/products').subscribe(data => {
      this.products = data;
    });
  }

}
