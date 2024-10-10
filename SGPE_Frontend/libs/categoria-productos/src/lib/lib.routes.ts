import { Route } from '@angular/router';
import { CategoriaProductosComponent } from './pages/categoria-productos/categoria-productos.component';
import { CategoriaEditComponent } from './pages/categoria-edit/categoria-edit.component';

export const categoriaproductosRoutes: Route[] = [
  { path:'', pathMatch:'full', component: CategoriaProductosComponent, title: 'Categoria Producto' },
  { path:'crear', pathMatch:'full', component: CategoriaEditComponent, title: 'Crear Categoria' },
  { path:'editar/:id', pathMatch:'full', component: CategoriaEditComponent, title: 'Editar Categoria' },
];
