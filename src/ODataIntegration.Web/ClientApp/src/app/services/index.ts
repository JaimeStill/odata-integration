import { CoreService } from './core.service';
import { ProductService } from './product.service';
import { SnackerService } from './snacker.service';
import { ThemeService } from './theme.service';

export const Services = [
  CoreService,
  ProductService,
  SnackerService,
  ThemeService
];

export * from './core.service';
export * from './product.service';
export * from './snacker.service';
export * from './theme.service';
