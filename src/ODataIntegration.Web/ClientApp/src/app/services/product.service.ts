import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject } from 'rxjs';
import { SnackerService } from './snacker.service';

import {
  ODataResult,
  Product
} from '../models';

@Injectable()
export class ProductService {
  private products = new BehaviorSubject<ODataResult<Product>>(null);

  products$ = this.products.asObservable();

  constructor(
    private http: HttpClient,
    private snacker: SnackerService
  ) { }

  getProducts = (query: string = '') => this.http.get<ODataResult<Product>>(`/odata/products${query}`)
    .subscribe(
      data => this.products.next(data),
      err => this.snacker.sendErrorMessage(err.error.error.message)
    );
}
