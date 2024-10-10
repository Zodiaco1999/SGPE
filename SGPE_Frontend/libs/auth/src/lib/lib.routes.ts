import { Route } from '@angular/router';
import { IniciarSesionComponent } from './pages/iniciar-sesion/iniciar-sesion.component';
import { RecuperarContrasenaComponent } from './pages/recuperar-contrasena/recuperar-contrasena.component';
import { AuthLayoutComponent } from './components/auth-layout/auth-layout.component';

export const authRoutes: Route[] = [
  {
    path: '',
    component: AuthLayoutComponent,
    children:  [
      {
        path: '',
        component: IniciarSesionComponent,
        title: 'Iniciar sesión'
      },
      {
        path: 'recuperarcontrasena',
        component: RecuperarContrasenaComponent,
        title: 'Iniciar sesión'
      }
    ]
  }
];
