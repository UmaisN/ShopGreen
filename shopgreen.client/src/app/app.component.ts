import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'ShopGreen';
  apiUrl = 'https://localhost:7035/api/products'; 
  products: any[] = [];
  selectedProduct: any = null

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getAllProducts();
  }

  getAllProducts() {
    this.http.get(this.apiUrl).subscribe(
      (data: any) => this.products = data,
      (error: any) => console.error(error)
    );
  }

  addProduct(product: any) {
    const headers = new HttpHeaders().set('Content-Type', 'application/json');
    const body = {
      name: product.name,
      description: product.description,
      price: product.price,
      quantity: product.quantity
    };

    this.http.post(this.apiUrl, body, { headers: headers }).subscribe(
      (response) => {
        console.log('Product created', response);
        this.getAllProducts();
      },
      (error) => {
        console.error('Error creating product:', error);
      }
    );
  }


  selectProduct(product: any) {
    this.selectedProduct = { ...product }; 
  }

  updateProduct(product: any) {
    this.http.put(`${this.apiUrl}/${product.productId}`, product).subscribe(
      () => {
        this.getAllProducts(); 
        this.selectedProduct = null; 
        console.log('Product updated successfully');
      },
      (error: any) => console.error(error)
    );
  }


  deleteProduct(productId: number) {
    this.http.delete(`${this.apiUrl}/${productId}`).subscribe(
      () => this.products = this.products.filter(p => p.productId !== productId),
      (error: any) => console.error(error)
    );
  }
}
