import { Route } from '@angular/router';
import { MainLayoutComponent } from './main-layout/main-layout.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { authGuard } from '@sgpe-ws/auth';

export const appRoutes: Route[] = [
  {
    path: '',
    component: MainLayoutComponent,
    loadChildren: () =>
      import('./main-layout/main-layout.module').then((m) => m.MainLayoutModule),
    canActivateChild: [authGuard]
  },
  {
    path: 'auth',
    loadChildren: () =>
      import('@sgpe-ws/auth').then((m) => m.AuthModule),
    canActivateChild: [authGuard]
  },
  {
    path: '**',
    component: MainLayoutComponent,
    children: [
      {
        path: '',
        pathMatch: 'full',
        component: NotFoundComponent,
        title: 'No encontrado'
      }
    ]
  }
];
