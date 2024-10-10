import { Route } from '@angular/router';
import { ProductosComponent } from './pages/productos/productos.component';
import { ProductoEditComponent } from './pages/producto-edit/producto-edit.component';

export const productosRoutes: Route[] = [
  { path: '', pathMatch: 'full', component: ProductosComponent, title: 'Productos' },
  { path: 'crear', pathMatch: 'full', component: ProductoEditComponent, title: 'Crear producto' },
  { path: 'editar/:id', pathMatch: 'full', component: ProductoEditComponent, title: 'Editar producto' }
];
