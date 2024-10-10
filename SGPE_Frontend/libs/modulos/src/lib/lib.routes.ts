import { Route } from '@angular/router';
import { ModulosComponent } from './pages/modulos/modulos.component';
import { ModuloEditComponent } from './pages/modulo-edit/modulo-edit.component';
import { ModuloViewComponent } from './pages/modulo-view/modulo-view.component';

export const modulosRoutes: Route[] = [
  { path: '', pathMatch: 'full', component: ModulosComponent, title: 'Modulos' },
  { path: 'crear', pathMatch: 'full', component: ModuloEditComponent, title: 'Crear modulo' },
  { path: 'editar/:id', pathMatch: 'full', component: ModuloEditComponent, title: 'Editar modulo' },
  { path: 'detalle/:id', pathMatch: 'full', component: ModuloViewComponent, title: 'Detalle modulo' }
];
