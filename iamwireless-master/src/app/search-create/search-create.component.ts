import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Router} from '@angular/router';
import {
  FormGroup, FormsModule, ReactiveFormsModule, FormBuilder,
  Validators, FormControl
} from '@angular/forms';


@Component({
  selector: 'app-search-create',
  templateUrl: './search-create.component.html',
  styleUrls: ['./search-create.component.css']
})
export class SearchCreateComponent implements OnInit {
  search: Search;


  constructor(private http: HttpClient, private router: Router) {
    this.search = new Search();
  }

  // createForm() {
  //   this.searchForm = this.fb.group({
  //     url: ['', Validators.required],
  //     keywords: ['', Validators.required],
  //     description: ['', Validators.required]
  //   });
  // }

  ngOnInit() {
  }

  // private prepareSave(): any {
  //   let input = new FormData();
  //   input.append('url', this.searchForm.get('url').value);
  //   input.append('keywords', this.searchForm.get('keywords').value);
  //   input.append('description', this.searchForm.get('description').value);
  //   console.log(this.searchForm.get('url').value);
  //   return input;
  // }

  saveSearch() {
    console.log(this.search);
    this.http.post('/search', this.search)
      .subscribe(res => {
          const id = res['_id'];
          this.router.navigate(['/home']);
        }, (err) => {
          console.log(err);
        }
      );
  }


}

export class Search {

  url: string;
  searchKeywords: string;
  description: string;

}
