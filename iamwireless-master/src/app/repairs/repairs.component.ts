import { Component, OnInit } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {isLoop} from 'tslint';

@Component({
  selector: 'app-repairs',
  templateUrl: './repairs.component.html',
  styleUrls: ['./repairs.component.css']
})
export class RepairsComponent implements OnInit {


  breadcrumbs = ['Repairs'];

  manufacturer: any;

  constructor(private http: HttpClient) {
  }

  navBread(name, isforward) {console.log(name);
       if (isforward) {
         this.breadcrumbs.push(name);
         } else {
                  for(let n = this.breadcrumbs.length - 1; n > 0; n--) {
                        if(this.breadcrumbs[n] !== name) {
                               this.breadcrumbs.pop();
                        }
                        else {
                              this.breadcrumbs.pop();
                                 break;
                        }
                  }

       }

    switch (this.breadcrumbs.length) {

      case 1:
        this.ngOnInit();
          break;

      case 2:
        this.getCategoryManufacturer(name);
        break;

     case 3:
        this.getManufacturer(name);
       break;

      case 4:
        this.getManufacturerModels(name);
        break;

      case 5:


    }


}

  ngOnInit() {
    this.http.get('/products/category').subscribe(data => {
      this.manufacturer = data;
      this.breadcrumbs = ['Repairs'];
      this.breadcrumbs.push( name );
    });
  }

  getCategoryManufacturer(name) {
    this.http.get('/products/category/' + name).subscribe(data => {
      this.manufacturer = data;
      this.breadcrumbs.push( name );
    });
  }


  getCarrierModels(name) {
    this.http.get('/products/carrier/' + name).subscribe(data => {
      this.manufacturer = data;
      this.breadcrumbs.push( name );
    });
  }

  getManufacturerModels(name) {
    this.http.get('/products/manufacturer/' + name).subscribe(data => {
      this.manufacturer = data;
      this.breadcrumbs.push( name );
    });
  }

  getManufacturer(name) {
    this.http.get('/products/manufacturer').subscribe(data => {
      this.manufacturer = data;
      this.breadcrumbs.push( name );
    });
  }
}
