import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import {
  FormGroup, FormsModule, ReactiveFormsModule, FormBuilder,
  Validators, FormControl
} from '@angular/forms';


@Component({
  selector: 'app-product-create',
  templateUrl: './product-create.component.html',
  styleUrls: ['./product-create.component.css'],
  encapsulation: ViewEncapsulation.None
})
export class ProductCreateComponent implements OnInit {

  productForm: FormGroup;


  constructor(private http: HttpClient, private router: Router, private fb: FormBuilder) {
    this.createForm();
  }

  createForm() {
    this.productForm = this.fb.group({
        category: ['', Validators.required],
        carrier: ['', Validators.required],
        manufacturer: ['', Validators.required],
        model: ['', Validators.required],
        description: ['', Validators.required],
        price: [0, Validators.required],
        imageUpload: null
    });
  }

  ngOnInit() {
  }

  onFileChange(event) {
    if (event.target.files.length > 0) {
      const file = event.target.files[0];
      this.productForm.get('imageUpload').setValue(file);
    }
  }

  private prepareSave(): any {
    let input = new FormData();
    input.append('category', this.productForm.get('category').value);
    input.append('carrier', this.productForm.get('carrier').value);
    input.append('manufacturer', this.productForm.get('manufacturer').value);
    input.append('model', this.productForm.get('model').value);
    input.append('description', this.productForm.get('description').value);
    input.append('price', this.productForm.get('price').value);
    input.append('imageUpload', this.productForm.get('imageUpload').value);
    console.log(this.productForm.get('category').value);
    return input;
  }

  saveProduct() {
    let formModel = this.prepareSave();
    this.http.post('/products', formModel)
      .subscribe(res => {
          const id = res['_id'];
          this.router.navigate(['/product-detail', id]);
        }, (err) => {
          console.log(err);
        }
      );
  }


}
