import { Route } from '@angular/router';
import { PerfilesComponent } from './pages/perfiles/perfiles.component';
import { PerfilEditComponent } from './pages/perfil-edit/perfil-edit.component';
import { PerfilViewComponent } from './pages/perfil-view/perfil-view.component';

export const perfilesRoutes: Route[] = [
  { path: '', pathMatch: 'full', component: PerfilesComponent, title: 'Perfiles' },
  { path: 'crear', pathMatch: 'full', component: PerfilEditComponent, title: 'Crear modulo' },
  { path: 'editar/:id', pathMatch: 'full', component: PerfilEditComponent, title: 'Editar modulo' },
  { path: 'detalle/:id', pathMatch: 'full', component: PerfilViewComponent, title: 'Detalle modulo' }
];
