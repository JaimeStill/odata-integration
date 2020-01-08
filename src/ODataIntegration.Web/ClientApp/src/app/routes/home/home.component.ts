import {
  Component,
  OnInit
} from '@angular/core';

import { ProductService } from '../../services';

@Component({
  selector: 'home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit {
  query = '';

  constructor(
    public productSvc: ProductService
  ) { }

  ngOnInit() {
    this.productSvc.getProducts(this.query);
  }

  updateProducts = () => this.productSvc.getProducts(this.query);
}
