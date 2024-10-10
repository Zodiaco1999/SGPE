import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { usuariosRoutes } from './lib.routes';
import { UsuariosComponent } from './pages/usuarios/usuarios.component';
import { UsuarioEditComponent } from './pages/usuario-edit/usuario-edit.component';
import { GeneralModule } from '@sgpe-ws/general';
import { LayoutUiModule } from '@sgpe-ws/layout-ui';
import { CambiarContrasenaComponent } from './pages/cambiar-contrasena/cambiar-contrasena.component';
import { CambiarCorreoComponent } from './pages/cambiar-correo/cambiar-correo.component';
import { UsuarioViewComponent } from './pages/usuario-view/usuario-view.component';

@NgModule({
  imports: [
    CommonModule,
    GeneralModule,
    LayoutUiModule,
    RouterModule.forChild(usuariosRoutes),
  ],
  declarations: [
    UsuariosComponent,
    UsuarioEditComponent,
    CambiarContrasenaComponent,
    CambiarCorreoComponent,
    UsuarioViewComponent,
  ],
})
export class UsuariosModule {}
