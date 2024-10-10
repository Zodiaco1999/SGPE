import { Route } from '@angular/router';
import { UsuariosComponent } from './pages/usuarios/usuarios.component';
import { UsuarioEditComponent } from './pages/usuario-edit/usuario-edit.component';
import { CambiarContrasenaComponent } from './pages/cambiar-contrasena/cambiar-contrasena.component';
import { CambiarCorreoComponent } from './pages/cambiar-correo/cambiar-correo.component';
import { UsuarioViewComponent } from './pages/usuario-view/usuario-view.component';

export const usuariosRoutes: Route[] = [
    { path:'', pathMatch:'full', component: UsuariosComponent, title: 'Usuarios' },
    { path: 'crear', pathMatch: 'full', component: UsuarioEditComponent, title: 'Crear usuario' },
    { path: 'editar/:id', pathMatch: 'full', component: UsuarioEditComponent, title: 'Editar usuario' },
    { path: 'detalle/:id', pathMatch: 'full', component: UsuarioViewComponent, title: 'Detalle usuario' },
    { path: 'cambiarcontrasena', pathMatch: 'full', component: CambiarContrasenaComponent, title: 'Cambiar contraseña' },
    { path: 'cambiarcorreo', pathMatch: 'full', component: CambiarCorreoComponent, title: 'Cambiar correo' },
    { path: 'restablecercorreo/:email/:token', pathMatch: 'full', component: CambiarCorreoComponent, title: 'Restablecer correo' },
    { path: 'restablecercontrasena/:email/:token', pathMatch: 'full', component: CambiarContrasenaComponent, title: 'Restablecer contraseña' },
];
